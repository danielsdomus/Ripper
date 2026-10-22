using UnityEngine;

public class PityLootSystem : MonoBehaviour
{
    [Header("Base Rates")]
    [SerializeField] [Range(0f, 1f)] float basePrototypeChance = 0.02f;

    [Header("Pity System Settings")]
    [SerializeField] int softPityThreshold = 15; // Number of packs after which the prototype chance starts to increase
    [SerializeField] int hardPityThreshold = 35; // Number of packs after which the prototype chance is guaranteed
    [SerializeField] [Range(0f, 1f)] float pityRampPerPack = 0.009f; // Incremental increase in prototype chance per pack after soft pity

    [Header("Non-Prototype Splits (Used If Prototype is Not Pulled)")]
    [SerializeField] [Range(0f, 1f)] float common = 0.6f;
    [SerializeField] [Range(0f, 1f)] float uncommon = 0.2f;
    [SerializeField] [Range(0f, 1f)] float rare = 0.1f;
    [SerializeField] [Range(0f, 1f)] float superRare = 0.05f;
    [SerializeField] [Range(0f, 1f)] float ultraRare = 0.03f;

    int pullsSinceLastPrototype = 0;

    float NonPrototypeTotalWeight => common + uncommon + rare + superRare + ultraRare;

    public LootRarity Roll() => Roll(Random.value);

    public LootRarity Roll(float roll01)
    {
        pullsSinceLastPrototype++;
        var prototypeChance = CalculatePrototypeChance();
        var roll = Mathf.Clamp01(roll01);

        if (roll <= prototypeChance)
        {
            pullsSinceLastPrototype = 0;
            return LootRarity.Prototype;
        }

        var nonPrototypeRoll = Mathf.InverseLerp(prototypeChance, 1f, roll);
        var commonThresh = common;
        var uncommonThresh = commonThresh + uncommon;
        var rareThresh = uncommonThresh + rare;
        var superRareThresh = rareThresh + superRare;
        var ultraRareThresh = superRareThresh + ultraRare;

        return nonPrototypeRoll switch
        {
            var value when value <= commonThresh / NonPrototypeTotalWeight => LootRarity.Common,
            var value when value <= uncommonThresh / NonPrototypeTotalWeight => LootRarity.Uncommon,
            var value when value <= rareThresh / NonPrototypeTotalWeight => LootRarity.Rare,
            var value when value <= superRareThresh / NonPrototypeTotalWeight => LootRarity.SuperRare,
            _ => LootRarity.UltraRare
        };
    }

    public float CalculatePrototypeChance()
    {
        var chance = basePrototypeChance;

        if (pullsSinceLastPrototype > softPityThreshold)
        {
            var extraPity = pullsSinceLastPrototype - softPityThreshold;
            chance += extraPity * pityRampPerPack;
        }

        if (pullsSinceLastPrototype >= hardPityThreshold)
        {
            chance = 1f; // Guarantee a prototype
        }

        return Mathf.Clamp01(chance);
    }

    public void ResetPity() => pullsSinceLastPrototype = 0;
}

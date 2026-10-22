using UnityEngine;

public enum LootRarity
{
    Common,
    Uncommon,
    Rare,
    SuperRare,
    UltraRare,
    Prototype
}

public class CardLootSystem : MonoBehaviour
{
    [Header("Drop Rates - Sum to 1.0")]
    [SerializeField] [Range(0.0f, 1.0f)] float common = 0.6f;
    [SerializeField] [Range(0.0f, 1.0f)] float uncommon = 0.2f;
    [SerializeField] [Range(0.0f, 1.0f)] float rare = 0.1f;
    [SerializeField] [Range(0.0f, 1.0f)] float superRare = 0.05f;
    [SerializeField] [Range(0.0f, 1.0f)] float ultraRare = 0.03f;
    [SerializeField] [Range(0.0f, 1.0f)] float prototype = 0.02f; 

    public float TotalWeight => common + uncommon + rare + superRare + ultraRare + prototype;

    void OnValidate()
    {
        if (Mathf.Abs(TotalWeight - 1.0f) > 0.0001f)
        {
            Debug.LogWarning("Drop rates in {nameof(CardLootSystem)} should sum to 1.0, but are {TotalWeight:F4}", this);
        }
    }

    public LootRarity Roll() => Roll(Random.value);

    public LootRarity Roll(float roll01)
    {
        var normalizedRoll = Mathf.Clamp01(roll01);
        var commonThresh = common;
        var uncommonThresh = commonThresh + uncommon;
        var rareThresh = uncommonThresh + rare;
        var superRareThresh = rareThresh + superRare;
        var ultraRareThresh = superRareThresh + ultraRare;

        return normalizedRoll switch
        {
            var value when value <= commonThresh => LootRarity.Common,
            var value when value <= uncommonThresh => LootRarity.Uncommon,
            var value when value <= rareThresh => LootRarity.Rare,
            var value when value <= superRareThresh => LootRarity.SuperRare,
            var value when value <= ultraRareThresh => LootRarity.UltraRare,
            _ => LootRarity.Prototype
        };
    }
}

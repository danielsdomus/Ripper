using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PackManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] PackSO pack;

    [Header("Pack Settings")]
    [SerializeField] int pityCap = 30; // max packs before opening pack's guaranteed rarity card

    int packsSinceLastPity = 0;

    void Awake()
    {
        if (!pack)
        {
            Debug.LogError("PackSO reference is missing in PackManager.");
            return;
        }
        packsSinceLastPity = PlayerPrefs.GetInt("Pity_" + pack.cardSet.setCode, 0); // Replace with proper save system
    }

    public List<CardSO> OpenPack()
    {
        List<CardSO> packCards = new List<CardSO>();
        List<CardRaritySO> rolledRarities = new List<CardRaritySO>();

        bool pityActive = packsSinceLastPity >= pityCap -1; // Guarantee pity card ON cap number.

        // Roll for each card in the pack
        for (int i = 0; i < pack.packSize; i++)
        {
            CardRaritySO rarity = (pityActive && i == pack.packSize - 1) ? pack.cardSet.pityRarity : RollRarity(pack.cardSet);
            rolledRarities.Add(rarity);
            CardSO card = rarity.cardPool[Random.Range(0, rarity.cardPool.Length)];
            packCards.Add(card);
        }
        packsSinceLastPity = rolledRarities.Contains(pack.cardSet.pityRarity) ? 0 : packsSinceLastPity++;

        Debug.Log("opened pack");
        pack.packOpenVFX.Play();
        return packCards;
    }

    private CardRaritySO RollRarity(CardSetSO cardSet)
    {
        float total = 0f;
        foreach (CardRaritySO rarity in cardSet.rarities) total += rarity.weight;

        float roll = Random.Range(0f, total);
        foreach (CardRaritySO rarity in cardSet.rarities)
        {
            roll -= rarity.weight;
            if (roll <= 0f) return rarity;
        }
        return cardSet.rarities[cardSet.rarities.Length - 1]; // fallback to the last rarity if something goes wrong
    }

    // void SpawnCards()
    // {
    //     List<Card> packCards = new List<Card>();
    //     for (int i = 0; i < cardsPerPack; i++)
    //     {
    //         Card cardToSpawn = Instantiate(currentPack.GetCardPrefab(), new Vector3(i * 2, 0, 0), Quaternion.identity);
    //         packCards.Add(cardToSpawn);
    //         Debug.Log($"Card {i} spawned");
    //     }
    // }
}

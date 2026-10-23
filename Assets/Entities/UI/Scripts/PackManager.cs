using UnityEngine;
using System.Collections.Generic;

public class PackManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] PackSO pack;

    [Header("Pack Settings")]
    [SerializeField] int pityCap = 30; // max packs before opening pack's guaranteed rarity card

    SaveData data;
    int packsSinceLastPity = 0;

    void Awake()
    {
        data = SaveSystem.Load();
        if (!pack)
        {
            Debug.LogError("PackSO reference is missing in PackManager.");
            return;
        }
        packsSinceLastPity = PlayerPrefs.GetInt("Pity_" + pack.cardSet.setCode, 0); // Replace with proper save system
    }

    public void OpenPack(PackSO pack)
    {
        CardSetSO cardSet = pack.cardSet;


        // Guards
        if (!cardSet)
        {
            Debug.LogError($"{pack.name}: CardSetSO reference is missing in PackSO. Aborting pack opening.");
            return;
        }
        if (!cardSet.pityRarity)
        {
            Debug.LogError($"{pack.name}: Pity rarity is not set in CardSetSO. Aborting pack opening.");
            return;
        }

        int pityCount = GetPityCount(cardSet.setCode);
        bool pityActive = pityCount >= cardSet.pityCap; // Guarantee pity card ON

        List<CardRaritySO> rolledRarities = new List<CardRaritySO>();

        for (int i = 0; i < pack.packSize; i++)
        {
            if (pityActive && i == pack.packSize - 1)
            {
                rolledRarities.Add(cardSet.pityRarity);
            } else
            {
                rolledRarities.Add(RollRarity(cardSet));
            }
        }
        int newPityCount = rolledRarities.Contains(cardSet.pityRarity) ? 0 : pityCount + 1;
        SetPityCount(cardSet.setCode, newPityCount);

        foreach (CardRaritySO rarity in rolledRarities)
            GrantCardFromRarity(rarity);
        
        data.totalPacksOpened++;
        SaveSystem.Save(data);
        Debug.Log($"Pack opened. Total packs opened: {data.totalPacksOpened}. Pity count for set {cardSet.setCode}: {newPityCount}");
        }

    // public List<CardSO> OpenPack()
    // {
    //     List<CardSO> packCards = new List<CardSO>();
    //     List<CardRaritySO> rolledRarities = new List<CardRaritySO>();

    //     bool pityActive = packsSinceLastPity >= pityCap -1; // Guarantee pity card ON cap number.

    //     // Roll for each card in the pack
    //     for (int i = 0; i < pack.packSize; i++)
    //     {
    //         CardRaritySO rarity = (pityActive && i == pack.packSize - 1) ? pack.cardSet.pityRarity : RollRarity(pack.cardSet);
    //         rolledRarities.Add(rarity);
    //         CardSO card = rarity.cardPool[Random.Range(0, rarity.cardPool.Length)];
    //         packCards.Add(card);
    //     }
    //     packsSinceLastPity = rolledRarities.Contains(pack.cardSet.pityRarity) ? 0 : packsSinceLastPity + 1;

    //     Debug.Log("opened pack");
    //     pack.packOpenVFX.Play();
    //     return packCards;
    // }

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

    private void GrantCardFromRarity(CardRaritySO rarity)
    {
        if (rarity.cardPool == null || rarity.cardPool.Length == 0)
        {
            Debug.LogError($"Rarity {rarity.name} has no cards in its pool. Rarity slot skipped.");
            return;
        }

        CardSO card = rarity.cardPool[Random.Range(0, rarity.cardPool.Length)];
        // TODO: add the card to the player's collection or inventory.
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

    private int GetPityCount(string setCode)
    {
        PitySaveEntry entry = data.pityCounters.Find(e => e.setCode == setCode);
        return entry != null ? entry.count : 0;
    }

    private void SetPityCount(string setCode, int count)
    {
        PitySaveEntry entry = data.pityCounters.Find(e => e.setCode == setCode);
        if (entry == null)
        {
            entry = new PitySaveEntry { setCode = setCode };
            data.pityCounters.Add(entry);
        }
        entry.count = count;
    }

}

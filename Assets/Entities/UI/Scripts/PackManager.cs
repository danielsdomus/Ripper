using UnityEngine;
using System.Collections.Generic;

public class PackManager : MonoBehaviour
{
    SaveData data;

    void Awake()
    {
        data = SaveSystem.Load();
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

        if (cardSet.rarities == null || cardSet.rarities.Length == 0)
        {
            Debug.LogError($"{cardSet.name} in {pack.name}: No rarities defined in CardSetSO. Aborting pack opening.");
            return;
        }

        int pityCount = GetPityCount(cardSet.setCode);
        bool pityActive = pityCount >= cardSet.pityCap - 1; // Guarantee pity card ON

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

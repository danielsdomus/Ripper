using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PackManager : MonoBehaviour
{
    Pack currentPack;

    void Start()
    {
        currentPack = FindFirstObjectByType<Pack>();
        Debug.Log(currentPack.GetPackName());
        Debug.Log(currentPack.GetPackSize());
    }

    void OpenPack()
    {
        Debug.Log("opened pack");
        currentPack.PlayPackOpenFX();
        SpawnCards();
    }

    void SpawnCards()
    {
        List<Card> packCards = new List<Card>();
        for (int i = 0; i < currentPack.GetPackSize(); i++)
        {
            Card cardToSpawn = Instantiate(currentPack.GetCardPrefab(), new Vector3(i * 2, 0, 0), Quaternion.identity);
            packCards.Add(cardToSpawn);
            Debug.Log($"Card {i} spawned");
        }
    }
}

using System;
using UnityEngine;

public class Pack : MonoBehaviour
{
    [Header("References")]
    [SerializeField] PackSO packSO;
    [SerializeField] Transform FXSpawnPos;
    
    [Header("Pack Settings")]
    [SerializeField] GameObject packOpenVFX;
    [SerializeField] Material packMaterial;

    private void Start()
    {
        if (!packSO) return;
        AssignMaterial();
        Debug.Log($"PackSO found.\nPack Name: {packSO.packName}\nPack Description:{packSO.packDescription}");
        Debug.Log($"Pack Price: {packSO.packPrice}\nPack Set: {packSO.cardSet}");
        Debug.Log($"Pack Size: {packSO.packSize}");
    }

    private void AssignMaterial()
    {
        packMaterial.mainTexture = packSO.packArt;
        this.GetComponent<Renderer>().material = packMaterial;
    }

    private void Update()
    {
        if (!packSO) return;
        //Debug.Log($"PackSO found. Pack Name: {packSO.packName}\n");
    }

    public string GetPackName()
    {
        return packSO.packName;
    }

    public string GetPackDescription()
    {
        return packSO.packDescription;
    }

    public CardSetSO GetPackSet()
    {
        return packSO.cardSet;
    }

    public int GetPackSize()
    {
        return packSO.packSize;
    }

    public float GetPackPrice()
    {
        return packSO.packPrice;
    }

    public void PlayPackOpenFX()
    {
        Instantiate(packOpenVFX, FXSpawnPos.position, Quaternion.identity);
    }

    internal Card GetCardPrefab()
    {
        throw new NotImplementedException();
    }
}

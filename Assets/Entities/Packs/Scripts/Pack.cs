using System;
using Unity.Mathematics;
using UnityEditor.UI;
using UnityEngine;

public class Pack : MonoBehaviour
{
    [SerializeField] PackSO packSO;
    [SerializeField] GameObject packOpenVFX;
    [SerializeField] Transform FXSpawnPos;

    [SerializeField] Material packMaterial;

    private void Start()
    {
        if (!packSO) return;
        AssignMaterial();
        Debug.Log($"PackSO found.\nPack Name: {packSO.packName}\nPack Description:{packSO.packDescription}");
        Debug.Log($"Pack Price: {packSO.packPrice}\nPack Set: {packSO.packSet}");
        Debug.Log($"Pack Size: {packSO.packSize}");
    }

    private void AssignMaterial()
    {
        packMaterial.mainTexture = packSO.packImage;
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

    public string GetPackSet()
    {
        return packSO.packSet;
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

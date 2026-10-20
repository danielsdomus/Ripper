using UnityEngine;

public class Pack : MonoBehaviour
{
    [SerializeField] PackSO packSO;
    [SerializeField] GameObject packOpenVFX;

    private void Start()
    {
        if (packSO == null)
            return;
        Debug.Log($"PackSO found.\nPack Name: {packSO.packName}\nPack Description:{packSO.packDescription}");
        Debug.Log($"Pack Price: {packSO.packPrice}\nPack Set: {packSO.packSet}");
        Debug.Log($"Pack Size: {packSO.packSize}");
    }

    private void Update()
    {
        if (packSO == null)
            return;
        //Debug.Log($"PackSO found. Pack Name: {packSO.packName}\n");
    }

}

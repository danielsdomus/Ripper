using UnityEngine;
using System.Collections;

public class PackManager : MonoBehaviour
{
    Pack currentPack;
    [SerializeField] GameObject packOpenVFX;

    void Awake()
    {
        
    }

    void Start()
    {
        currentPack = FindFirstObjectByType<Pack>();
        Debug.Log(currentPack.GetPackName());
        Debug.Log(currentPack.GetPackSize());
    }

    public void OpenPack()
    {
        Debug.Log("opened pack");
        Instantiate(packOpenVFX, transform.position, Quaternion.identity);
    }
}

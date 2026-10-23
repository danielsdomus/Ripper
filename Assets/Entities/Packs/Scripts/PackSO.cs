using UnityEngine;

[CreateAssetMenu(fileName = "PackSO", menuName = "Scriptable Objects/PackSO", order = 1)]
public class PackSO : ScriptableObject
{
    public GameObject packPrefab;
    public string packName;
    public string packDescription;
    [Header("Card Data")]
    public CardSetSO cardSet;
    public float packPrice = 8.99f;
    public int packSize = 10;
    [Header("Art")]
    public Texture packArt;
    public ParticleSystem packOpenVFX;
}

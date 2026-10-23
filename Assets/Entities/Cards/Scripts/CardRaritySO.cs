using UnityEngine;

[CreateAssetMenu(fileName = "CardRaritySO", menuName = "Scriptable Objects/CardRarity", order = 2)]
public class CardRaritySO : ScriptableObject
{
    public string rarityName;
    public float weight; // higher weight means more common, lower weight means rarer
    public CardSO[] cardPool;
}

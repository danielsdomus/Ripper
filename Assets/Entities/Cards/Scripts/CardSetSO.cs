using UnityEngine;

[CreateAssetMenu(fileName = "CardSetSO", menuName = "Scriptable Objects/CardSet", order = 3)]
public class CardSetSO : ScriptableObject
{
    public string setName;
    public string setCode;
    public string setDescription;
    public int pityCap = 30;
    public CardRaritySO[] rarities; // this set's rarity pools
    public CardRaritySO pityRarity; // the rarity that is guaranteed to be pulled after a certain
                                    //  number of packs have been opened without pulling a card of that rarity
}

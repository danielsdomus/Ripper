using UnityEngine;

[CreateAssetMenu(fileName = "CardSO", menuName = "Scriptable Objects/CardSO", order = 0)]
public class CardSO : ScriptableObject
{
    public GameObject cardPrefab;
   
   [Header("Card Info")]
    public string cardName;
    [TextArea] public string cardDescription;
    
    [Header("Card Data")]
    public CardSetSO cardSet;
    public int cardID;
    public float cardPrice;
    public CardType cardType;
    
    [Header("Art")]
    public Texture2D cardArt;
    public CardStyle cardStyle;

    public enum CardType
    {
        Creature,
        Location,
        Object,
        Spell,
        Effect
    }

    public enum CardStyle
    {
        Normal,
        Foil,
        FullArt,
        Prototype
    }

}

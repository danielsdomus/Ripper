using UnityEngine;

[CreateAssetMenu(fileName = "CardSO", menuName = "Scriptable Objects/CardSO", order = 0)]
public class CardSO : ScriptableObject
{
    public GameObject cardPrefab;
    [Header("Card Info")]
    public string cardName;
    public string cardDescription;
    [Header("Card Data")]
    public float cardPrice;
    public CardType cardType;
    public Rarity rarity;
    [Header("Art")]
    public string imageName;
    // Change this when I figure out how I want to handle images, right now simple name/directory lookup
    public CardStyle cardStyle;
    public ParticleSystem openPackVFX;

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

    public enum Rarity
    {
        Common,
        Uncommon,
        Rare,
        SuperRare,
        UltraRare
    }
}

using UnityEngine;

[CreateAssetMenu(fileName = "CardSO", menuName = "Scriptable Objects/CardSO", order = 0)]
public class CardSO : ScriptableObject
{
    public GameObject cardPrefab;
    public string cardName;
    public string cardDescription;
    public float cardPrice;
    public Rarity rarity;
    public CardStyle cardStyle;
    public string imageName;
    // Change this when I figure out how I want to handle images, right now simple name/directory lookup
    public CardType cardType;
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

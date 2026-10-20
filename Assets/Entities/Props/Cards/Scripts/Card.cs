using UnityEngine;

public class Card : MonoBehaviour
{

    [SerializeField] string cardName;
    [SerializeField] string cardDescription;
    [SerializeField] float cardPrice;
    [SerializeField] Rarity rarity;
    [SerializeField] CardStyle cardStyle;
    [SerializeField] string imageName; 
    // Change this when I figure out how I want to handle images, right now simple name/directory lookup
    [SerializeField] CardType cardType;

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

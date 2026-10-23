using UnityEngine;

public class Card : MonoBehaviour
{
    [SerializeField] CardSO cardSO;
    [SerializeField] Material cardMaterial;


    void Start()
    {
        if (!cardSO) return;
        Debug.Log(cardSO.cardName);
        AssignMaterial();
    }

    private void AssignMaterial()
    {
        cardMaterial.mainTexture = cardSO.cardArt;
        this.GetComponent<Renderer>().material = cardMaterial;
    }
}

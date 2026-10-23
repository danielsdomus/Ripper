using UnityEngine;

public class Card : MonoBehaviour
{
    [SerializeField] CardSO cardSO;
    [SerializeField] Material cardMaterial;


    void Start()
    {
        Debug.Log(cardSO.cardName);
        AssignMaterial();
    }

    private void AssignMaterial()
    {
        cardMaterial.mainTexture = cardSO.cardArt;
        this.GetComponent<Renderer>().material = cardMaterial;
    }
}

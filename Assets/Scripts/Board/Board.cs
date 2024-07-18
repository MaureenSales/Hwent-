using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Board : MonoBehaviour
{
    public GameObject WeatherArea;
    public List<Field> Fields { get; private set; }
    public List<GameObject> AllCardsObject
    {
        get
        {
            return GetAllCards();
        }
        set { }
    }
    public List<Card> AllCards
    {
        get
        {
            List<Card> result = new List<Card>();
            foreach (var card in AllCardsObject) result.Add(card.GetComponent<ThisCard>().thisCard);
            return result;
        }
        set { }
    }

    public GameObject FieldPlayer;
    public GameObject FieldEnemy;
    // Start is called before the first frame update
    void Start()
    {
        Fields = new List<Field>();
        Fields.Add(FieldPlayer.GetComponent<Field>());
        Fields.Add(FieldEnemy.GetComponent<Field>());
    }

    private List<GameObject> GetAllCards()
    {
        List<GameObject> result = new List<GameObject>();
        foreach (Field field in Fields)
        {
            Debug.Log(field.AllCardsObjects.Count);
            foreach (var card in field.AllCardsObjects)
            {
                result.Add(card);
            }
        }
        return result;
    }

}

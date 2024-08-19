using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Field : MonoBehaviour
{
    public List<Row> Rows { get; private set; }
    public List<Card> AllCards { get; private set; }
    public List<GameObject> AllCardsObjects { get; private set; }
    public GameObject PowerField { get; private set; }
    public Player Owner { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        Owner = this.transform.parent.parent.GetComponentInChildren<Player>();
        Rows = new List<Row>();
        AllCards = new List<Card>();
        AllCardsObjects = new List<GameObject>();
        PowerField = transform.GetChild(0).gameObject;
        for (int i = 1; i < this.transform.childCount; i++)
        {
            Rows.Add(this.transform.GetChild(i).GetComponentInChildren<Row>());
        }

    }
}

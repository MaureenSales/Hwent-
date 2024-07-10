using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    public List<GameObject> CardsObject = new();
    public List<Card> Cards = new();
    public Player Owner { get; private set;}
    // Start is called before the first frame update
    void Start()
    {
        Owner = this.transform.parent.parent.GetComponent<Player>();
    }
}

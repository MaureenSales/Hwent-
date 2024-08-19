using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Graveyard : MonoBehaviour
{
    public List<GameObject> CardsObject {get; private set;}
    public List<Card> Cards {get; private set;}
    public Player Owner {get; private set;}
    // Start is called before the first frame update
    void Start()
    {
        CardsObject = new List<GameObject>();
        Cards = new List<Card>();
        Owner = this.transform.parent.GetComponent<Player>();
    }

}

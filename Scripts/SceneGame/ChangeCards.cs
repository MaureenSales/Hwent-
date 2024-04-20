using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ChangeCards : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            GameObject owner = GetComponentInParent<Canvas>().GetComponent<GameController>().currentTurn;
            if (owner.transform.Find(owner.name + "Info").GetComponent<PlayerController>().cardsToChange.Count < 2)
            {
                owner.transform.Find(owner.name + "Info").GetComponent<PlayerController>().cardsToChange.Add(eventData.pointerClick.gameObject);
            }
        }
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}

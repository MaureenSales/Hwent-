using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ChangeCards : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        //selecionar las cartas a cambiar al inicio de la primera ronda
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            GameObject owner = GetComponentInParent<Canvas>().GetComponent<GameController>().currentTurn;
            if (owner.transform.Find(owner.name + "Info").GetComponent<PlayerController>().cardsToChange.Count < 2)
            {
                owner.transform.Find(owner.name + "Info").GetComponent<PlayerController>().cardsToChange.Add(eventData.pointerClick.gameObject);
            }
        }
    }

}

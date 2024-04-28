using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Threading.Tasks;

public class ClickOnCard : MonoBehaviour, IPointerClickHandler
{
    public GameObject ClearImages; //Imgenes de los rayos de sol de despeje
    async public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            //Activar efecto de despeje
            Debug.Log("EnterOnClickRigth");
            if (eventData.pointerClick.GetComponent<ThisCard>().thisCard is Clear && eventData.pointerClick.transform.parent.name == "Hand")
            {
                LeanTween.move(eventData.pointerClick.gameObject, new Vector3(549.9293f, 300.0388f, 0f), 2f);
                await Task.Delay(2000);
                ClearImages.SetActive(true);
                GetComponentInParent<Canvas>().GetComponent<GameController>().ClearWeatherZone();
                await Task.Delay(400);
                Destroy(eventData.pointerClick);
                ClearImages.SetActive(false);
                if (!GetComponentInParent<Canvas>().GetComponent<GameController>().notCurrentTurn.GetComponentInChildren<PlayerController>().Pass)
                {
                    GetComponentInParent<Canvas>().GetComponent<GameController>().FinalizedTurn();

                }
            }
            else if (eventData.pointerClick.GetComponent<ThisCard>().thisCard is DecoyUnit && eventData.pointerClick.transform.parent.name == "Hand")
            {
                //Activar efecto de señuelo
                GetComponentInParent<Canvas>().GetComponent<GameController>().Panel.gameObject.SetActive(true);
                GetComponentInParent<Canvas>().GetComponent<GameController>().DecoyFirstPart(eventData.pointerClick.gameObject);
            }

        }
        else
        {

            if (eventData.pointerClick.GetComponent<ThisCard>().borderLight.gameObject.activeSelf)
            {
                //Escoger la carta a cambiar con el señuelo
                if (eventData.pointerClick.transform.parent.name != "Hand")
                {
                    GetComponentInParent<Canvas>().GetComponent<GameController>().DecoySecondPart(eventData.pointerClick.gameObject);
                    GetComponentInParent<Canvas>().GetComponent<GameController>().Panel.gameObject.SetActive(false);
                    if (!GetComponentInParent<Canvas>().GetComponent<GameController>().notCurrentTurn.GetComponentInChildren<PlayerController>().Pass)
                    {
                        GetComponentInParent<Canvas>().GetComponent<GameController>().FinalizedTurn();
                    }

                }
            }
            else if (eventData.pointerClick.GetComponent<ThisCard>().thisCard is Leader && eventData.pointerClick.transform.parent.parent.name == GetComponentInParent<Canvas>().GetComponent<GameController>().currentTurn.name)
            {
                //Activar efecto de líder
                Leader leader = (Leader)eventData.pointerClick.GetComponent<ThisCard>().thisCard;
                if (leader.Faction == Global.Factions.Gryffindor)
                {
                    GetComponentInParent<Canvas>().GetComponent<GameController>().GryffindorEffect();
                }
                else if (leader.Faction == Global.Factions.Slytherin)
                {
                    GetComponentInParent<Canvas>().GetComponent<GameController>().SlytherinEffect();
                }
                this.enabled = false;
                if (!GetComponentInParent<Canvas>().GetComponent<GameController>().notCurrentTurn.GetComponentInChildren<PlayerController>().Pass)
                {
                    GetComponentInParent<Canvas>().GetComponent<GameController>().FinalizedTurn();
                }

            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        ClearImages = GetComponentInParent<Canvas>().GetComponent<GameController>().ClearImages;

    }
}

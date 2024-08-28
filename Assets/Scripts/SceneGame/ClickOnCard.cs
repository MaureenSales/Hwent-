using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Threading.Tasks;
using Unity.VisualScripting;

public class ClickOnCard : MonoBehaviour, IPointerClickHandler
{
    public GameObject ClearImages; //Imgenes de los rayos de sol de despeje
    async public void OnPointerClick(PointerEventData eventData)
    {
        ClearImages = GetComponentInParent<Canvas>().GetComponent<GameController>().ClearImages;
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            //Activar efecto de despeje
            if (eventData.pointerClick.GetComponent<ThisCard>().thisCard is Clear && eventData.pointerClick.transform.parent.name == "Hand")
            {
                GameObject ClearCard = eventData.pointerClick.gameObject;
                LeanTween.move(ClearCard, new Vector3(800f, 400f, 0f), 2f);
                await Task.Delay(2000);
                for (int i = 0; i < ClearImages.transform.childCount; i++)
                {
                    Color color = ClearImages.transform.GetChild(i).GetComponent<Image>().color;
                    color.a = 0f;
                    ClearImages.transform.GetChild(i).GetComponent<Image>().color = color;
                }
                ClearImages.SetActive(true);
                for (int i = 0; i < ClearImages.transform.childCount; i++)
                {
                    LeanTween.alpha(ClearImages.transform.GetChild(i).GetComponent<RectTransform>(), 1, 2f);
                }
                GetComponentInParent<Canvas>().GetComponent<GameController>().currentTurn.GetComponent<Player>().MyHand.CardsObject.Remove(ClearCard);
                GetComponentInParent<Canvas>().GetComponent<GameController>().currentTurn.GetComponent<Player>().MyHand.Cards.Remove(ClearCard.GetComponent<ThisCard>().thisCard);
                GetComponentInParent<Canvas>().GetComponent<GameController>().ClearWeatherZone();
                await Task.Delay(1000);
                ClearCard.transform.localScale = new Vector3(0.9f, 0.9f, 0f);
                LeanTween.move(ClearCard, GetComponentInParent<Canvas>().GetComponent<GameController>().currentTurn.transform.Find("Graveyard").position, 1f).setOnComplete(() => ClearCard.transform.SetParent(GetComponentInParent<Canvas>().GetComponent<GameController>().currentTurn.transform.Find("Graveyard")));
                GetComponentInParent<Canvas>().GetComponent<GameController>().currentTurn.GetComponent<Player>().MyGraveyard.CardsObject.Add(ClearCard);
                GetComponentInParent<Canvas>().GetComponent<GameController>().currentTurn.GetComponent<Player>().MyGraveyard.Cards.Add(ClearCard.GetComponent<ThisCard>().thisCard);
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
                else
                {
                    GetComponentInParent<Canvas>().GetComponent<GameController>().Effects(eventData.pointerClick.gameObject);
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
        
    }
}

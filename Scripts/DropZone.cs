using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.EventSystems;

public class DropZone : MonoBehaviour, IDropHandler
{
   public void OnDrop(PointerEventData eventData)
   {
      UnityEngine.Debug.Log(eventData.pointerDrag.name + " was dropped on " + gameObject.name);

      Drag card = eventData.pointerDrag.GetComponent<Drag>();
      UnityEngine.Debug.Log(card.parentToReturnTo.name);
      if (card != null)
      {

         if (card.parentToReturnTo.parent.parent == this.transform.parent.parent.parent)
         {
            if (this.name == "MeleeZone" && (this.transform.childCount < 10))
            {
               if (eventData.pointerDrag.GetComponent<ThisCard>().thisCard is Unit && eventData.pointerDrag.GetComponent<ThisCard>().attackType.Contains(Global.AttackModes.Melee))
               {
                  this.transform.gameObject.GetComponent<Row>().AddToRow(eventData.pointerDrag.gameObject);
                  card.originalScale = new Vector3(0.9f, 0.9f, 0f);
                  card.parentToReturnTo.GetComponent<HandCards>().RemoveCard(eventData.pointerDrag.gameObject);
                  card.parentToReturnTo = this.transform;
                  StartCoroutine(EnableDragScript(eventData.pointerDrag.GetComponent<Drag>()));
                  //eventData.pointerDrag.GetComponent<Info>().enabled = false;
                  GameObject.Find("WeatherZone").GetComponent<WeatherController>().WeatherEffect(eventData.pointerDrag.gameObject, this.transform);
                  Improve(eventData, "Melee");
                  GetComponentInParent<Canvas>().GetComponent<GameController>().FinalizedTurn(this.transform.parent.parent.parent.gameObject);
               }
               else if (eventData.pointerDrag.GetComponent<ThisCard>().thisCard is HeroUnit && eventData.pointerDrag.GetComponent<ThisCard>().attackType.Contains(Global.AttackModes.Melee))
               {
                  this.transform.gameObject.GetComponent<Row>().AddToRow(eventData.pointerDrag.gameObject);
                  card.originalScale = new Vector3(0.9f, 0.9f, 0f);
                  card.parentToReturnTo.GetComponent<HandCards>().RemoveCard(eventData.pointerDrag.gameObject);
                  card.parentToReturnTo = this.transform;
                  StartCoroutine(EnableDragScript(eventData.pointerDrag.GetComponent<Drag>()));
                  //eventData.pointerDrag.GetComponent<Info>().enabled = false;

                  GetComponentInParent<Canvas>().GetComponent<GameController>().FinalizedTurn(this.transform.parent.parent.parent.gameObject);
               }

            }
            else if (this.name == "RangedZone" && (this.transform.childCount < 10))
            {
               if (eventData.pointerDrag.GetComponent<ThisCard>().thisCard is Unit && eventData.pointerDrag.GetComponent<ThisCard>().attackType.Contains(Global.AttackModes.Ranged))
               {
                  this.transform.gameObject.GetComponent<Row>().AddToRow(eventData.pointerDrag.gameObject);
                  card.originalScale = new Vector3(0.9f, 0.9f, 0f);
                  card.parentToReturnTo.GetComponent<HandCards>().RemoveCard(eventData.pointerDrag.gameObject);
                  card.parentToReturnTo = this.transform;
                  StartCoroutine(EnableDragScript(eventData.pointerDrag.GetComponent<Drag>()));
                  //eventData.pointerDrag.GetComponent<Info>().enabled = false;
                  GameObject.Find("WeatherZone").GetComponent<WeatherController>().WeatherEffect(eventData.pointerDrag.gameObject, this.transform);
                  Improve(eventData, "Ranged");
                  GetComponentInParent<Canvas>().GetComponent<GameController>().FinalizedTurn(this.transform.parent.parent.parent.gameObject);
               }
               else if (eventData.pointerDrag.GetComponent<ThisCard>().thisCard is HeroUnit && eventData.pointerDrag.GetComponent<ThisCard>().attackType.Contains(Global.AttackModes.Ranged))
               {
                  this.transform.gameObject.GetComponent<Row>().AddToRow(eventData.pointerDrag.gameObject);
                  card.originalScale = new Vector3(0.9f, 0.9f, 0f);
                  card.parentToReturnTo.GetComponent<HandCards>().RemoveCard(eventData.pointerDrag.gameObject);
                  card.parentToReturnTo = this.transform;
                  StartCoroutine(EnableDragScript(eventData.pointerDrag.GetComponent<Drag>()));
                  //eventData.pointerDrag.GetComponent<Info>().enabled = false;

                  GetComponentInParent<Canvas>().GetComponent<GameController>().FinalizedTurn(this.transform.parent.parent.parent.gameObject);
               }
            }
            else if (this.name == "SiegeZone" && (this.transform.childCount < 10))
            {
               if (eventData.pointerDrag.GetComponent<ThisCard>().thisCard is Unit && eventData.pointerDrag.GetComponent<ThisCard>().attackType.Contains(Global.AttackModes.Siege))
               {
                  this.transform.gameObject.GetComponent<Row>().AddToRow(eventData.pointerDrag.gameObject);
                  card.originalScale = new Vector3(0.9f, 0.9f, 0f);
                  card.parentToReturnTo.GetComponent<HandCards>().RemoveCard(eventData.pointerDrag.gameObject);
                  card.parentToReturnTo = this.transform;
                  StartCoroutine(EnableDragScript(eventData.pointerDrag.GetComponent<Drag>()));
                  //eventData.pointerDrag.GetComponent<Info>().enabled = false;
                  GameObject.Find("WeatherZone").GetComponent<WeatherController>().WeatherEffect(eventData.pointerDrag.gameObject, this.transform);
                  Improve(eventData, "Siege");
                  GetComponentInParent<Canvas>().GetComponent<GameController>().FinalizedTurn(this.transform.parent.parent.parent.gameObject);
               }
               else if (eventData.pointerDrag.GetComponent<ThisCard>().thisCard is HeroUnit && eventData.pointerDrag.GetComponent<ThisCard>().attackType.Contains(Global.AttackModes.Siege))
               {
                  this.transform.gameObject.GetComponent<Row>().AddToRow(eventData.pointerDrag.gameObject);
                  card.originalScale = new Vector3(0.9f, 0.9f, 0f);
                  card.parentToReturnTo.GetComponent<HandCards>().RemoveCard(eventData.pointerDrag.gameObject);
                  card.parentToReturnTo = this.transform;
                  StartCoroutine(EnableDragScript(eventData.pointerDrag.GetComponent<Drag>()));
                  //eventData.pointerDrag.GetComponent<Info>().enabled = false;

                  GetComponentInParent<Canvas>().GetComponent<GameController>().FinalizedTurn(this.transform.parent.parent.parent.gameObject);

               }

            }
            else if (this.name == "BoostMelee" && eventData.pointerDrag.GetComponent<ThisCard>().thisCard is Boost && this.transform.childCount < 2)
            {
               card.originalScale = new Vector3(0.9f, 0.9f, 0f);
               card.parentToReturnTo.GetComponent<HandCards>().RemoveCard(eventData.pointerDrag.gameObject);
               card.parentToReturnTo = this.transform;
               StartCoroutine(EnableDragScript(eventData.pointerDrag.GetComponent<Drag>()));
               //eventData.pointerDrag.GetComponent<Info>().enabled = false;

               GetComponentInParent<Canvas>().GetComponent<GameController>().ImproveUnits(this.transform.parent.GetComponentInChildren<Row>().unitObjects);
               GetComponentInParent<Canvas>().GetComponent<GameController>().FinalizedTurn(this.transform.parent.parent.parent.gameObject);
            }
            else if (this.name == "BoostRanged" && eventData.pointerDrag.GetComponent<ThisCard>().thisCard is Boost && this.transform.childCount < 2)
            {
               card.originalScale = new Vector3(0.9f, 0.9f, 0f);
               card.parentToReturnTo.GetComponent<HandCards>().RemoveCard(eventData.pointerDrag.gameObject);
               card.parentToReturnTo = this.transform;
               StartCoroutine(EnableDragScript(eventData.pointerDrag.GetComponent<Drag>()));
               //eventData.pointerDrag.GetComponent<Info>().enabled = false;

               GetComponentInParent<Canvas>().GetComponent<GameController>().ImproveUnits(this.transform.parent.GetComponentInChildren<Row>().unitObjects);
               GetComponentInParent<Canvas>().GetComponent<GameController>().FinalizedTurn(this.transform.parent.parent.parent.gameObject);
            }
            else if (this.name == "BoostSiege" && eventData.pointerDrag.GetComponent<ThisCard>().thisCard is Boost && this.transform.childCount < 2)
            {
               card.originalScale = new Vector3(0.9f, 0.9f, 0f);
               card.parentToReturnTo.GetComponent<HandCards>().RemoveCard(eventData.pointerDrag.gameObject);
               card.parentToReturnTo = this.transform;
               StartCoroutine(EnableDragScript(eventData.pointerDrag.GetComponent<Drag>()));
               //eventData.pointerDrag.GetComponent<Info>().enabled = false;

               GetComponentInParent<Canvas>().GetComponent<GameController>().ImproveUnits(this.transform.parent.GetComponentInChildren<Row>().unitObjects);
               GetComponentInParent<Canvas>().GetComponent<GameController>().FinalizedTurn(this.transform.parent.parent.parent.gameObject);
            }

         }

         if (this.name == "MeleeWeather" && eventData.pointerDrag.GetComponent<ThisCard>().thisCard is Weather && eventData.pointerDrag.GetComponent<ThisCard>().cardName == "Escarcha Heladora" && (this.transform.childCount < 1))
         {
            card.originalScale = new Vector3(0.8f, 0.8f, 0f);
            card.parentToReturnTo.GetComponent<HandCards>().RemoveCard(eventData.pointerDrag.gameObject);

            GetComponentInParent<Canvas>().GetComponent<GameController>().FinalizedTurn(card.parentToReturnTo.parent.parent.gameObject);

            card.parentToReturnTo = this.transform;
            StartCoroutine(EnableDragScript(eventData.pointerDrag.GetComponent<Drag>()));
            //eventData.pointerDrag.GetComponent<Info>().enabled = false;

            GameObject.Find("WeatherZone").GetComponent<WeatherController>().weather[0] = true;
            GameObject.Find("WeatherZone").GetComponent<WeatherController>().WeatherEffect(GameObject.Find("EnemyField").transform.Find("MeleeRow").GetComponentInChildren<Row>().unitObjects);
            GameObject.Find("WeatherZone").GetComponent<WeatherController>().WeatherEffect(GameObject.Find("PlayerField").transform.Find("MeleeRow").GetComponentInChildren<Row>().unitObjects);
            GameObject.Find("WeatherZone").GetComponent<WeatherController>().ApplyWeather("Frost");

         }
         else if (this.name == "RangedWeather" && eventData.pointerDrag.GetComponent<ThisCard>().thisCard is Weather && eventData.pointerDrag.GetComponent<ThisCard>().cardName == "Niebla Profunda" && (this.transform.childCount < 1))
         {
            card.originalScale = new Vector3(0.8f, 0.8f, 0f);
            card.parentToReturnTo.GetComponent<HandCards>().RemoveCard(eventData.pointerDrag.gameObject);

            GetComponentInParent<Canvas>().GetComponent<GameController>().FinalizedTurn(card.parentToReturnTo.parent.parent.gameObject);

            card.parentToReturnTo = this.transform;
            StartCoroutine(EnableDragScript(eventData.pointerDrag.GetComponent<Drag>()));
            //eventData.pointerDrag.GetComponent<Info>().enabled = false;

            GameObject.Find("WeatherZone").GetComponent<WeatherController>().weather[1] = true;
            GameObject.Find("WeatherZone").GetComponent<WeatherController>().WeatherEffect(GameObject.Find("EnemyField").transform.Find("RangedRow").GetComponentInChildren<Row>().unitObjects);
            GameObject.Find("WeatherZone").GetComponent<WeatherController>().WeatherEffect(GameObject.Find("PlayerField").transform.Find("RangedRow").GetComponentInChildren<Row>().unitObjects);
            GameObject.Find("WeatherZone").GetComponent<WeatherController>().ApplyWeather("Fog");
         }
         else if (this.name == "SiegeWeather" && eventData.pointerDrag.GetComponent<ThisCard>().thisCard is Weather && eventData.pointerDrag.GetComponent<ThisCard>().cardName == "Diluvio Quidditch" && (this.transform.childCount < 1))
         {
            card.originalScale = new Vector3(0.8f, 0.8f, 0f);
            card.parentToReturnTo.GetComponent<HandCards>().RemoveCard(eventData.pointerDrag.gameObject);

            GetComponentInParent<Canvas>().GetComponent<GameController>().FinalizedTurn(card.parentToReturnTo.parent.parent.gameObject);

            card.parentToReturnTo = this.transform;
            StartCoroutine(EnableDragScript(eventData.pointerDrag.GetComponent<Drag>()));
            //eventData.pointerDrag.GetComponent<Info>().enabled = false;

            GameObject.Find("WeatherZone").GetComponent<WeatherController>().weather[2] = true;
            GameObject.Find("WeatherZone").GetComponent<WeatherController>().WeatherEffect(GameObject.Find("EnemyField").transform.Find("SiegeRow").GetComponentInChildren<Row>().unitObjects);
            GameObject.Find("WeatherZone").GetComponent<WeatherController>().WeatherEffect(GameObject.Find("PlayerField").transform.Find("SiegeRow").GetComponentInChildren<Row>().unitObjects);
            GameObject.Find("WeatherZone").GetComponent<WeatherController>().ApplyWeather("Rain");
         }


         UnityEngine.Debug.Log(this.name);

      }
   }
   private IEnumerator EnableDragScript(Drag drag)
   {
      yield return new WaitForEndOfFrame();
      drag.enabled = false;
   }

   private void Improve(PointerEventData eventData, string zone)
   {
      GameObject boost = this.transform.parent.Find("Boost" + zone).gameObject;
      UnityEngine.Debug.Log("EnterImprove");
      UnityEngine.Debug.Log(boost.transform.childCount);
      if ((boost.transform.childCount != 0) && (boost.name == "BoostMelee"))
      {
         GetComponentInParent<Canvas>().GetComponent<GameController>().ImproveUnits(eventData.pointerDrag.gameObject, zone);
      }
      else if ((boost.transform.childCount != 0) && (boost.name == "BoostRanged"))
      {
         GetComponentInParent<Canvas>().GetComponent<GameController>().ImproveUnits(eventData.pointerDrag.gameObject, zone);
      }
      else if ((boost.transform.childCount != 0) && (boost.name == "BoostSiege"))
      {
         GetComponentInParent<Canvas>().GetComponent<GameController>().ImproveUnits(eventData.pointerDrag.gameObject, zone);
      }
   }

   private void SortingOrdenRender(GameObject card)
   {

   }


}

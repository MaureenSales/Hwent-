using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DropZone : MonoBehaviour, IDropHandler
{
   public void OnDrop(PointerEventData eventData)
   {
      Drag card = eventData.pointerDrag.GetComponent<Drag>(); //componente Drag de la carta soltada
      Hand hand = eventData.pointerDrag.transform.parent.GetComponent<Hand>();
      if (card != null)
      {
         //!!!Disminuir codigo!!!

         if (card.parentToReturnTo.parent.parent == this.transform.parent.parent.parent) //verifica que la carta este siendo soltada en el campo del jugador actual usando la jerarquía del campo en la escena 
         {
            if (this.name == "MeleeZone" && (this.transform.childCount < 10))
            {
               if (eventData.pointerDrag.GetComponent<ThisCard>().thisCard is Unit && eventData.pointerDrag.GetComponent<ThisCard>().attackType.Contains(Global.AttackModes.Melee))
               {
                  this.transform.gameObject.GetComponent<Row>().AddToRow(eventData.pointerDrag.gameObject);
                  card.originalScale = new Vector3(0.9f, 0.9f, 0f);
                  card.parentToReturnTo = this.transform;
                  hand.CardsObject.Remove(eventData.pointerDrag);
                  hand.Cards.Remove(eventData.pointerDrag.GetComponent<ThisCard>().thisCard);
                  StartCoroutine(EnableDragScript(eventData.pointerDrag.GetComponent<Drag>()));

                  GetComponentInParent<Canvas>().GetComponent<GameController>().Effects(eventData.pointerDrag.gameObject);
                  //GameObject.Find("WeatherZone").GetComponent<WeatherController>().WeatherEffect(eventData.pointerDrag.gameObject, this.transform);
                  GetComponentInParent<Canvas>().GetComponent<GameController>().Improve(eventData.pointerDrag.gameObject, "Melee");
                  if (!GetComponentInParent<Canvas>().GetComponent<GameController>().notCurrentTurn.GetComponentInChildren<PlayerController>().Pass)
                  {
                     GetComponentInParent<Canvas>().GetComponent<GameController>().FinalizedTurn();
                  }
               }
               else if (eventData.pointerDrag.GetComponent<ThisCard>().thisCard is HeroUnit && eventData.pointerDrag.GetComponent<ThisCard>().attackType.Contains(Global.AttackModes.Melee))
               {
                  this.transform.gameObject.GetComponent<Row>().AddToRow(eventData.pointerDrag.gameObject);
                  card.originalScale = new Vector3(0.9f, 0.9f, 0f);
                  card.parentToReturnTo = this.transform;
                  hand.CardsObject.Remove(eventData.pointerDrag);
                  hand.Cards.Remove(eventData.pointerDrag.GetComponent<ThisCard>().thisCard);
                  StartCoroutine(EnableDragScript(eventData.pointerDrag.GetComponent<Drag>()));

                  GetComponentInParent<Canvas>().GetComponent<GameController>().Effects(eventData.pointerDrag.gameObject);
                  if (!GetComponentInParent<Canvas>().GetComponent<GameController>().notCurrentTurn.GetComponentInChildren<PlayerController>().Pass)
                  {
                     GetComponentInParent<Canvas>().GetComponent<GameController>().FinalizedTurn();
                  }
               }

            }
            else if (this.name == "RangedZone" && (this.transform.childCount < 10))
            {
               if (eventData.pointerDrag.GetComponent<ThisCard>().thisCard is Unit && eventData.pointerDrag.GetComponent<ThisCard>().attackType.Contains(Global.AttackModes.Ranged))
               {
                  this.transform.gameObject.GetComponent<Row>().AddToRow(eventData.pointerDrag.gameObject);
                  card.originalScale = new Vector3(0.9f, 0.9f, 0f);
                  card.parentToReturnTo = this.transform;
                  hand.CardsObject.Remove(eventData.pointerDrag);
                  hand.Cards.Remove(eventData.pointerDrag.GetComponent<ThisCard>().thisCard);
                  StartCoroutine(EnableDragScript(eventData.pointerDrag.GetComponent<Drag>()));

                  GetComponentInParent<Canvas>().GetComponent<GameController>().Effects(eventData.pointerDrag.gameObject);
                  //GameObject.Find("WeatherZone").GetComponent<WeatherController>().WeatherEffect(eventData.pointerDrag.gameObject, this.transform);
                  GetComponentInParent<Canvas>().GetComponent<GameController>().Improve(eventData.pointerDrag.gameObject, "Ranged");
                  if (!GetComponentInParent<Canvas>().GetComponent<GameController>().notCurrentTurn.GetComponentInChildren<PlayerController>().Pass)
                  {
                     GetComponentInParent<Canvas>().GetComponent<GameController>().FinalizedTurn();
                  }
               }
               else if (eventData.pointerDrag.GetComponent<ThisCard>().thisCard is HeroUnit && eventData.pointerDrag.GetComponent<ThisCard>().attackType.Contains(Global.AttackModes.Ranged))
               {
                  this.transform.gameObject.GetComponent<Row>().AddToRow(eventData.pointerDrag.gameObject);
                  card.originalScale = new Vector3(0.9f, 0.9f, 0f);
                  card.parentToReturnTo = this.transform;
                  hand.CardsObject.Remove(eventData.pointerDrag);
                  hand.Cards.Remove(eventData.pointerDrag.GetComponent<ThisCard>().thisCard);
                  StartCoroutine(EnableDragScript(eventData.pointerDrag.GetComponent<Drag>()));

                  GetComponentInParent<Canvas>().GetComponent<GameController>().Effects(eventData.pointerDrag.gameObject);
                  if (!GetComponentInParent<Canvas>().GetComponent<GameController>().notCurrentTurn.GetComponentInChildren<PlayerController>().Pass)
                  {
                     GetComponentInParent<Canvas>().GetComponent<GameController>().FinalizedTurn();
                  }
               }
            }
            else if (this.name == "SiegeZone" && (this.transform.childCount < 10))
            {
               if (eventData.pointerDrag.GetComponent<ThisCard>().thisCard is Unit && eventData.pointerDrag.GetComponent<ThisCard>().attackType.Contains(Global.AttackModes.Siege))
               {
                  this.transform.gameObject.GetComponent<Row>().AddToRow(eventData.pointerDrag.gameObject);
                  card.originalScale = new Vector3(0.9f, 0.9f, 0f);
                  card.parentToReturnTo = this.transform;
                  hand.CardsObject.Remove(eventData.pointerDrag);
                  hand.Cards.Remove(eventData.pointerDrag.GetComponent<ThisCard>().thisCard);
                  StartCoroutine(EnableDragScript(eventData.pointerDrag.GetComponent<Drag>()));

                  GetComponentInParent<Canvas>().GetComponent<GameController>().Effects(eventData.pointerDrag.gameObject);
                  //GameObject.Find("WeatherZone").GetComponent<WeatherController>().WeatherEffect(eventData.pointerDrag.gameObject, this.transform);
                  GetComponentInParent<Canvas>().GetComponent<GameController>().Improve(eventData.pointerDrag.gameObject, "Siege");
                  if (!GetComponentInParent<Canvas>().GetComponent<GameController>().notCurrentTurn.GetComponentInChildren<PlayerController>().Pass)
                  {
                     GetComponentInParent<Canvas>().GetComponent<GameController>().FinalizedTurn();
                  }
               }
               else if (eventData.pointerDrag.GetComponent<ThisCard>().thisCard is HeroUnit && eventData.pointerDrag.GetComponent<ThisCard>().attackType.Contains(Global.AttackModes.Siege))
               {
                  this.transform.gameObject.GetComponent<Row>().AddToRow(eventData.pointerDrag.gameObject);
                  card.originalScale = new Vector3(0.9f, 0.9f, 0f);
                  card.parentToReturnTo = this.transform;
                  hand.CardsObject.Remove(eventData.pointerDrag);
                  hand.Cards.Remove(eventData.pointerDrag.GetComponent<ThisCard>().thisCard);
                  StartCoroutine(EnableDragScript(eventData.pointerDrag.GetComponent<Drag>()));
            
                  GetComponentInParent<Canvas>().GetComponent<GameController>().Effects(eventData.pointerDrag.gameObject);
                  if (!GetComponentInParent<Canvas>().GetComponent<GameController>().notCurrentTurn.GetComponentInChildren<PlayerController>().Pass)
                  {
                     GetComponentInParent<Canvas>().GetComponent<GameController>().FinalizedTurn();
                  }

               }

            }
            else if (this.name == "BoostMelee" && eventData.pointerDrag.GetComponent<ThisCard>().thisCard is Boost && this.transform.childCount < 2)
            {
               card.originalScale = new Vector3(0.9f, 0.9f, 0f);
               card.parentToReturnTo = this.transform;
               hand.CardsObject.Remove(eventData.pointerDrag);
               hand.Cards.Remove(eventData.pointerDrag.GetComponent<ThisCard>().thisCard);
               StartCoroutine(EnableDragScript(eventData.pointerDrag.GetComponent<Drag>()));

               GetComponentInParent<Canvas>().GetComponent<GameController>().ImproveUnits(this.transform.parent.GetComponentInChildren<Row>().unitObjects, this.transform.parent);
               if (!GetComponentInParent<Canvas>().GetComponent<GameController>().notCurrentTurn.GetComponentInChildren<PlayerController>().Pass)
               {
                  GetComponentInParent<Canvas>().GetComponent<GameController>().FinalizedTurn();
               }
            }
            else if (this.name == "BoostRanged" && eventData.pointerDrag.GetComponent<ThisCard>().thisCard is Boost && this.transform.childCount < 2)
            {
               card.originalScale = new Vector3(0.9f, 0.9f, 0f);
               card.parentToReturnTo = this.transform;
               hand.CardsObject.Remove(eventData.pointerDrag);
               hand.Cards.Remove(eventData.pointerDrag.GetComponent<ThisCard>().thisCard);
               StartCoroutine(EnableDragScript(eventData.pointerDrag.GetComponent<Drag>()));

               GetComponentInParent<Canvas>().GetComponent<GameController>().ImproveUnits(this.transform.parent.GetComponentInChildren<Row>().unitObjects, this.transform.parent);
               if (!GetComponentInParent<Canvas>().GetComponent<GameController>().notCurrentTurn.GetComponentInChildren<PlayerController>().Pass)
               {
                  GetComponentInParent<Canvas>().GetComponent<GameController>().FinalizedTurn();
               }
            }
            else if (this.name == "BoostSiege" && eventData.pointerDrag.GetComponent<ThisCard>().thisCard is Boost && this.transform.childCount < 2)
            {
               card.originalScale = new Vector3(0.9f, 0.9f, 0f);
               card.parentToReturnTo = this.transform;
               hand.CardsObject.Remove(eventData.pointerDrag);
               hand.Cards.Remove(eventData.pointerDrag.GetComponent<ThisCard>().thisCard);
               StartCoroutine(EnableDragScript(eventData.pointerDrag.GetComponent<Drag>()));

               GetComponentInParent<Canvas>().GetComponent<GameController>().ImproveUnits(this.transform.parent.GetComponentInChildren<Row>().unitObjects, this.transform.parent);
               if (!GetComponentInParent<Canvas>().GetComponent<GameController>().notCurrentTurn.GetComponentInChildren<PlayerController>().Pass)
               {
                  GetComponentInParent<Canvas>().GetComponent<GameController>().FinalizedTurn();
               }
            }

         }

         if (this.name == "MeleeWeather" && eventData.pointerDrag.GetComponent<ThisCard>().thisCard is Weather && eventData.pointerDrag.GetComponent<ThisCard>().thisCard.Skills[0].Name == "WeatherMelee" && (this.transform.childCount < 1))
         {
            card.originalScale = new Vector3(0.8f, 0.8f, 0f);
            card.parentToReturnTo = this.transform;
            hand.CardsObject.Remove(eventData.pointerDrag);
            hand.Cards.Remove(eventData.pointerDrag.GetComponent<ThisCard>().thisCard);
            StartCoroutine(EnableDragScript(eventData.pointerDrag.GetComponent<Drag>()));

            GameObject.Find("WeatherZone").GetComponent<WeatherController>().weather[0] = true;
            //GameObject.Find("WeatherZone").GetComponent<WeatherController>().WeatherEffect(GameObject.Find("EnemyField").transform.Find("MeleeRow").GetComponentInChildren<Row>().unitObjects, "Enemy");
            //GameObject.Find("WeatherZone").GetComponent<WeatherController>().WeatherEffect(GameObject.Find("PlayerField").transform.Find("MeleeRow").GetComponentInChildren<Row>().unitObjects, "Player");
            GameObject.Find("WeatherZone").GetComponent<WeatherController>().ApplyWeather("Frost");
            if (!GetComponentInParent<Canvas>().GetComponent<GameController>().notCurrentTurn.GetComponentInChildren<PlayerController>().Pass)
            {
               GetComponentInParent<Canvas>().GetComponent<GameController>().FinalizedTurn();
            }

         }
         else if (this.name == "RangedWeather" && eventData.pointerDrag.GetComponent<ThisCard>().thisCard is Weather && eventData.pointerDrag.GetComponent<ThisCard>().thisCard.Skills[0].Name == "WeatherRanged" && (this.transform.childCount < 1))
         {
            card.originalScale = new Vector3(0.8f, 0.8f, 0f);
            card.parentToReturnTo = this.transform;
            hand.CardsObject.Remove(eventData.pointerDrag);
            hand.Cards.Remove(eventData.pointerDrag.GetComponent<ThisCard>().thisCard);
            StartCoroutine(EnableDragScript(eventData.pointerDrag.GetComponent<Drag>()));

            GameObject.Find("WeatherZone").GetComponent<WeatherController>().weather[1] = true;
            //GameObject.Find("WeatherZone").GetComponent<WeatherController>().WeatherEffect(GameObject.Find("EnemyField").transform.Find("RangedRow").GetComponentInChildren<Row>().unitObjects, "Enemy");
            GameObject.Find("WeatherZone").GetComponent<WeatherController>().ApplyWeather("Fog");
            //GameObject.Find("WeatherZone").GetComponent<WeatherController>().WeatherEffect(GameObject.Find("PlayerField").transform.Find("RangedRow").GetComponentInChildren<Row>().unitObjects, "Player");
            if (!GetComponentInParent<Canvas>().GetComponent<GameController>().notCurrentTurn.GetComponentInChildren<PlayerController>().Pass)
            {
               GetComponentInParent<Canvas>().GetComponent<GameController>().FinalizedTurn();
            }
         }
         else if (this.name == "SiegeWeather" && eventData.pointerDrag.GetComponent<ThisCard>().thisCard is Weather && eventData.pointerDrag.GetComponent<ThisCard>().thisCard.Skills[0].Name == "WeatherSiege" && (this.transform.childCount < 1))
         {
            card.originalScale = new Vector3(0.8f, 0.8f, 0f);
            card.parentToReturnTo = this.transform;
            hand.CardsObject.Remove(eventData.pointerDrag);
            hand.Cards.Remove(eventData.pointerDrag.GetComponent<ThisCard>().thisCard);
            StartCoroutine(EnableDragScript(eventData.pointerDrag.GetComponent<Drag>()));

            GameObject.Find("WeatherZone").GetComponent<WeatherController>().weather[2] = true;
            //GameObject.Find("WeatherZone").GetComponent<WeatherController>().WeatherEffect(GameObject.Find("EnemyField").transform.Find("SiegeRow").GetComponentInChildren<Row>().unitObjects, "Enemy");
            //GameObject.Find("WeatherZone").GetComponent<WeatherController>().WeatherEffect(GameObject.Find("PlayerField").transform.Find("SiegeRow").GetComponentInChildren<Row>().unitObjects, "Player");
            GameObject.Find("WeatherZone").GetComponent<WeatherController>().ApplyWeather("Rain");
            if (!GetComponentInParent<Canvas>().GetComponent<GameController>().notCurrentTurn.GetComponentInChildren<PlayerController>().Pass)
            {
               GetComponentInParent<Canvas>().GetComponent<GameController>().FinalizedTurn();
            }
         }

      }
   }

   /// <summary>
   /// Corutina para desactivar el arrastre de las cartas
   /// </summary>
   /// <param name="drag"></param>
   /// <returns></returns>
   private IEnumerator EnableDragScript(Drag drag)
   {
      yield return new WaitForEndOfFrame();
      if (drag != null)
      {
         drag.enabled = false;

      }
   }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
[System.Serializable]
public class ThisCard : MonoBehaviour
{
    public Card thisCard = null;
    public string cardName; //nombre
    public string skill; //habilidad
    public string power; //poder
    public List<Global.AttackModes> attackType; //lista con los ataques de las cartas
    public Sprite sprite; //sprite para imagen de la carta
    public Sprite faction; //sprite para icono de facción
    public Sprite cardType; //sprite para icono de tipo de carta
    public Sprite descriptionSilver; //sprite para imagen de área de descripción de carta de unidad de Plata
    public Sprite descriptionHero; //sprite para imagen de área de descripción de carta de unidad de Héroe
    public Image AttackTypeMelee; //icono para ataque cuerpo a cuerpo
    public Image AttackTypeRanged; //icono para ataque a distancia
    public Image AttackTypeSiege; //icono para asedio
    public Image Description; //imagen para área de descripción o habilidad
    public Image imageCardType; //icono para el tipo de Carta Especial
    public Image powerImage; //icono para el poder
    public Image borderLight; //marco de luz para resaltar la carta en casos específicos
    public Image borderFire; //marco de llamas de fuego para eliminar la carta

    public TextMeshProUGUI nameText; //texto UI para el nombre de la carta
    public TextMeshProUGUI skillText; //texto UI para la habilidad
    public TextMeshProUGUI powerText; //texto UI para el poder
    public Image image; //imagen de la carta
    public Image factionImage; //imagen del icono la facción

    /// <summary>
    /// Imprime en la UI de la carta la información de la misma
    /// </summary>
    /// <param name="card">carta a crear</param>
    public void PrintCard(Card card)
    {
        borderLight.gameObject.SetActive(false);
        borderFire.gameObject.SetActive(false);
        if (card != null)
        {
            thisCard = card;

            cardName = card.Name;
            if (card.Skills.Count == 1)
            {
                if (Global.Effects.ContainsKey(card.Skills[0].Name)) skill = Global.Effects[card.Skills[0].Name];
                else if (Global.SpecialsEffects.ContainsKey(card.Skills[0].Name)) skill = Global.SpecialsEffects[card.Skills[0].Name];
                else if(Global.EffectsCreated.ContainsKey(card.Skills[0].Name)) skill = card.Skills[0].Name;
            }
            else if (card.Skills.Count > 1)
            {
                foreach (var effect in card.Skills)
                {
                    skill += effect.Name + "\n";
                }
            }
            sprite = card.Image;

            nameText.text = cardName;
            skillText.text = skill;
            image.sprite = sprite;

            if (card.Faction == Global.Factions.Neutral)
            {
                factionImage.gameObject.SetActive(false);
            }
            else
            {
                faction = card.FactionImage;
                factionImage.sprite = faction;
            }

            if (card is UnitCard)
            {

                var unit = (UnitCard)card;

                power = unit.Power.ToString();
                powerText.text = power;
                imageCardType.gameObject.SetActive(false);
                attackType = unit.AttackTypes;


                if (!unit.AttackTypes.Contains(Global.AttackModes.Melee))
                {
                    AttackTypeMelee.gameObject.SetActive(false);
                }
                if (!unit.AttackTypes.Contains(Global.AttackModes.Ranged))
                {
                    AttackTypeRanged.gameObject.SetActive(false);
                }
                if (!unit.AttackTypes.Contains(Global.AttackModes.Siege))
                {
                    AttackTypeSiege.gameObject.SetActive(false);
                }


            }
            if (card is SpecialCard)
            {
                var special = (SpecialCard)card;
                AttackTypeMelee.gameObject.SetActive(false);
                AttackTypeRanged.gameObject.SetActive(false);
                AttackTypeSiege.gameObject.SetActive(false);
                powerText.gameObject.SetActive(false);
                cardType = special.CardType;
                imageCardType.sprite = cardType;

            }
            if (card is Leader)
            {
                AttackTypeMelee.gameObject.SetActive(false);
                AttackTypeRanged.gameObject.SetActive(false);
                AttackTypeSiege.gameObject.SetActive(false);
                powerText.gameObject.SetActive(false);
                powerImage.gameObject.SetActive(false);
                imageCardType.gameObject.SetActive(false);
                if (this.name.StartsWith("CardPrefab 1"))
                {
                    this.transform.GetComponent<DragChooseGroup>().enabled = false;
                }
                else
                {
                    this.transform.GetComponent<Drag>().enabled = false;
                }
            }
            if (card is HeroUnit)
            {
                Description.sprite = descriptionHero;
            }
            else
            {
                Description.sprite = descriptionSilver;
            }

        }

    }


    // Update is called once per frame
    void Update()
    {

    }
}

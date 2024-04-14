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
    public string cardName;
    public string skill;
    public string power;
    public List<Global.AttackModes> attackType;
    public Sprite sprite;
    public Sprite faction;
    public Sprite cardType;
    public Sprite descriptionSilver;
    public Sprite descriptionHero;
    public Image AttackTypeMelee;
    public Image AttackTypeRanged;
    public Image AttackTypeSiege;
    public Image Description;
    public Image imageCardType;
    public Image powerImage;
    public Image border;

    public TextMeshProUGUI nameText;
    public TextMeshProUGUI skillText;
    public TextMeshProUGUI powerText;
    public Image image;
    public Image factionImage;

    // Start is called before the first frame update
    void Start()
    {
    
    }

    public void PrintCard(Card card)
    {
        border.gameObject.SetActive(false);
        if (card != null)
        {
            thisCard = card;

            cardName = card.Name;
            skill = card.Skill;
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
            }
            if(card is DecoyUnit)
            {
                
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

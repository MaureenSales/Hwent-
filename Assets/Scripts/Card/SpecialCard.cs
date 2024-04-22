using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SpecialCard : Card
{
    public Sprite CardType;
    public SpecialCard(string name, string skill, string description, Sprite image, Sprite cardType) : 
    base(name, Global.Factions.Neutral, skill, description, image)
    {
        CardType = cardType;
    }

}

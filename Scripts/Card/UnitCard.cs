using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UnitCard : Card
{
    public int Power {get;}
    public List<Global.AttackModes> AttackTypes { get; protected set; }

    protected UnitCard(string name, Global.Factions faction, string skill, string description, int power, List<Global.AttackModes> attackTypes, Sprite image) :
    base(name, faction, skill, description, image)
    {
        this.Power = power;
        AttackTypes = attackTypes;
    }

}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Unit : UnitCard
{
    public Unit(string name, Global.Factions faction, string skill, string description, int power, List<Global.AttackModes> attackTypes, Sprite image) :
    base(name, faction, skill, description, power, attackTypes, image)
    {}

}
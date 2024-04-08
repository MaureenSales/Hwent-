using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeroUnit: UnitCard
{
    public HeroUnit(string name, Global.Factions faction, string skill, string description, int power, List<Global.AttackModes> attackType, Sprite image):
    base(name, faction, skill, description, power, attackType, image){}

    
}
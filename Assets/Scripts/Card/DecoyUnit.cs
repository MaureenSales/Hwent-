using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DecoyUnit : UnitCard
{
    /// <summary>
    /// Constructor de la clase de tipo señuelo
    /// </summary>
    /// <param name="name">nombre</param>
    /// <param name="description">descripción</param>
    /// <param name="image">imagen</param>
    public DecoyUnit(string name, string description, Sprite image) :
    base(name, Global.Factions.Neutral, new List<Skill>() { new Skill("Decoy", null, null)}, description, 0, new List<Global.AttackModes>() { Global.AttackModes.Siege, Global.AttackModes.Melee, Global.AttackModes.Ranged }, image)
    { }
}
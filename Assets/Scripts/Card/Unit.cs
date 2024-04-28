using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Unit : UnitCard
{
    /// <summary>
    /// Constructor de la clase de tipo Unidad o Unidad de Plata
    /// </summary>
    /// <param name="name">nombre</param>
    /// <param name="faction">facción</param>
    /// <param name="skill">habilidad</param>
    /// <param name="description">descripción</param>
    /// <param name="power">poder</param>
    /// <param name="attackTypes">tipos de ataque</param>
    /// <param name="image">imagen</param>
    public Unit(string name, Global.Factions faction, string skill, string description, int power, List<Global.AttackModes> attackTypes, Sprite image) :
    base(name, faction, skill, description, power, attackTypes, image)
    {}

}
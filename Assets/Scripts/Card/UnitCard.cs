using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UnitCard : Card
{
    public int Power {get;} //poder
    public List<Global.AttackModes> AttackTypes { get; protected set; } //tipos de ataque

    /// <summary>
    /// Constructor de la clase de tipo Carta de Unidad
    /// </summary>
    /// <param name="name">nombre</param>
    /// <param name="faction">facción</param>
    /// <param name="skill">habilidad</param>
    /// <param name="description">descripción</param>
    /// <param name="power">poder</param>
    /// <param name="attackTypes">tipos de ataque</param>
    /// <param name="image">imagen</param>
    protected UnitCard(string name, Global.Factions faction, string skill, string description, int power, List<Global.AttackModes> attackTypes, Sprite image) :
    base(name, faction, skill, description, image)
    {
        this.Power = power;
        AttackTypes = attackTypes;
    }

}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeroUnit: UnitCard
{
    /// <summary>
    /// Contructor de la clase de tipo Héroe
    /// </summary>
    /// <param name="name">nombre</param>
    /// <param name="faction">facción</param>
    /// <param name="skill">habilidad</param>
    /// <param name="description">descripción</param>
    /// <param name="power">número de poder</param>
    /// <param name="attackType">tipo de ataque</param>
    /// <param name="image">imagen</param>
    public HeroUnit(string name, Global.Factions faction, string skill, string description, int power, List<Global.AttackModes> attackType, Sprite image):
    base(name, faction, skill, description, power, attackType, image){}

    
}
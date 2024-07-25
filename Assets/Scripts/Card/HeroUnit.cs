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
    new public int Power { get => base.Power; private set {} }
    public HeroUnit(string name, Global.Factions faction, List<Skill> skills, string description, int power, List<Global.AttackModes> attackType, Sprite image):
    base(name, faction, skills, description, power, attackType, image){}

    public override object Clone()
    {
        return new HeroUnit(this.Name, this.Faction, this.Skills, this.Description, this.Power, this.AttackTypes, this.Image);
    }
}
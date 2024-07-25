using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Weather: SpecialCard
{
    /// <summary>
    /// Constructor de la clase de tipo Clima
    /// </summary>
    /// <param name="name">nombre</param>
    /// <param name="skill">habilidad</param>
    /// <param name="description">descripción</param>
    /// <param name="image">imagen</param>
    /// <param name="weatherType">icono del tipo de clima</param>
    public Weather(string name, List<Skill> skills, string description, Sprite image, Sprite weatherType): 
    base(name, skills, description, image, weatherType){}

    public override object Clone()
    {
        return new Weather(this.Name, this.Skills, this.Description, this.Image, this.CardType);
    }
}
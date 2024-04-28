using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Boost: SpecialCard
{
    /// <summary>
    /// Constructor para las cartas de tipo Aumento
    /// </summary>
    /// <param name="name">nombre</param>
    /// <param name="skill">habilidad</param>
    /// <param name="description">descripción</param>
    /// <param name="image">icono de aumento</param>
    public Boost(string name, string skill, string description, Sprite image): 
    base(name, skill, description, image, Resources.Load <Sprite> ("Boost")){}
}
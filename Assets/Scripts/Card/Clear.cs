using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Clear: SpecialCard
{
    /// <summary>
    /// Constructor de la clase de tipo Despeje
    /// </summary>
    /// <param name="name">nombre</param>
    /// <param name="skill">habilidad</param>
    /// <param name="description">descripción</param>
    /// <param name="image">icono de Despeje</param>
    public Clear(string name, string skill, string description, Sprite image): 
    base(name, skill, description, image, Resources.Load <Sprite> ("Clear")){}
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SpecialCard : Card
{
    public Sprite CardType; //icono que representa el tipo de carta espcial

    /// <summary>
    /// Constructor de la clase de tipo Carta Especial
    /// </summary>
    /// <param name="name">nombre</param>
    /// <param name="skill">habilidad</param>
    /// <param name="description">descripción</param>
    /// <param name="image">imagen</param>
    /// <param name="cardType">icono</param>
    public SpecialCard(string name, string skill, string description, Sprite image, Sprite cardType) : 
    base(name, Global.Factions.Neutral, skill, description, image)
    {
        CardType = cardType;
    }

}

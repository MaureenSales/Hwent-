using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Card
{
    public string Name { get; private set; } //nombre de la carta
    public Global.Factions Faction { get; private set; } //facción a la que pertenece
    public string Skill { get; private set; } //habilidad
    public string Description { get; private set; }  //descripción
    public Sprite Image {get; private set; } //imagen de la carta
    public Sprite FactionImage { get; private set; } //icono de la facción

/// <summary>
/// Constructor de la clase madre de tipo Carta
/// </summary>
    protected Card(string name, Global.Factions faction, string skill, string description, Sprite image)
    {
        Name = name;
        Faction = faction;
        Skill = skill;
        Description = description;
        Image = image;
        if(faction == Global.Factions.Gryffindor)
        {
            FactionImage = Resources.Load <Sprite> ("Gryffindor");
        }
        else if(faction == Global.Factions.Slytherin)
        {
            FactionImage = Resources.Load <Sprite> ("Slytherin");
        }
    }
 
}
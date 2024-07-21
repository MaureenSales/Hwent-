using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public abstract class Card
{
    public string Name { get; private set; } //nombre de la carta
    public Global.Factions Faction { get; private set; } //facción a la que pertenece
    public List<Skill> Skills; //habilidad
    public string Description { get; private set; }  //descripción
    public Sprite Image { get; private set; } //imagen de la carta
    public Sprite FactionImage { get; private set; } //icono de la facción
    public string Type => this.GetType().ToString();
    private string owner;
    public string Owner { get { return owner; } private set { owner = value; } }

    protected Card(string name, Global.Factions faction, List<Skill> skills, string description, Sprite image)
    {
        owner = string.Empty;
        Name = name;
        Faction = faction;
        Skills = skills;
        Description = description;
        Image = image;

        if (faction == Global.Factions.Gryffindor)
        {
            FactionImage = Resources.Load<Sprite>("Gryffindor");
        }
        else if (faction == Global.Factions.Slytherin)
        {
            FactionImage = Resources.Load<Sprite>("Slytherin");
        }
    }

    public void AssignOwner(string ownerId)
    {
        if(string.IsNullOrEmpty(owner)) owner = ownerId;
        //else throw new InvalidOperationException("El dueno ya ha sido asignado y no puede ser cambiado");
    }

}
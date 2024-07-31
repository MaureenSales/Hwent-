using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
public abstract class Card : ICloneable, IEquatable<Card>
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

        switch (faction)
        {
            case Global.Factions.Gryffindor:
                FactionImage = Resources.Load<Sprite>("Gryffindor"); break;
            case Global.Factions.Slytherin:
                FactionImage = Resources.Load<Sprite>("Slytherin"); break;
            case Global.Factions.Ravenclaw:
                FactionImage = Resources.Load<Sprite>("Ravenclaw"); break;
            case Global.Factions.Hufflepuff:
                FactionImage = Resources.Load<Sprite>("Hufflepuff"); break;
        }
    }

    public void AssignOwner(string ownerId)
    {
        if (string.IsNullOrEmpty(owner)) owner = ownerId;
        //else throw new InvalidOperationException("El dueno ya ha sido asignado y no puede ser cambiado");
    }

    public bool Equals(Card other)
    {
        if (this.Name != other.Name) return false;
        if (this.Faction != other.Faction) return false;
        if (other is UnitCard unit) return unit.Power == ((UnitCard)this).Power;
        else return true;
    }

    public abstract object Clone();
}
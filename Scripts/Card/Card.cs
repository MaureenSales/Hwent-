using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Card
{
    public string Name { get; private set; }
    public Global.Factions Faction { get; private set; }
    public string Skill { get; private set; }
    public string Description { get; private set; }
    public Sprite Image {get; private set; }
    public Sprite FactionImage { get; private set; }


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


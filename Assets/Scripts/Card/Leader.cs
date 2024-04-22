using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Leader : Card
{
    public Leader(string name, Global.Factions faction, string skill, string description, Sprite image) : 
    base(name, faction, skill, description, image){}
}

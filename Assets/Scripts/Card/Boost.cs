using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Boost: SpecialCard
{
    public Boost(string name, string skill, string description, Sprite image): 
    base(name, skill, description, image, Resources.Load <Sprite> ("Boost")){}
}
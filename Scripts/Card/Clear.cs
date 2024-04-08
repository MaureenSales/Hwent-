using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Clear: SpecialCard
{
    public Clear(string name, string skill, string description, Sprite image): 
    base(name, skill, description, image, Resources.Load <Sprite> ("Clear")){}
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Leader : Card
{
    /// <summary>
    /// Contructor de la clase de tipo Líder
    /// </summary>
    /// <param name="name">nombre</param>
    /// <param name="faction">facción</param>
    /// <param name="skill">habilidad</param>
    /// <param name="description">descripción</param>
    /// <param name="image">imagen</param>
    public Leader(string name, Global.Factions faction, string skill, string description, Sprite image) : 
    base(name, faction, skill, description, image){}
}

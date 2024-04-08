using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.UI;

public static class Global
{
    public static Dictionary<string, string> Effects = new Dictionary<string, string>
    {
        {"PutBoost", "Poner un aumento en una fila propia"},
        {"DrawCard", "Robar un carta"},
        {"PutWeather", "Poner un clima"},
        {"PowerfulCard", "Eliminar la carta con más poder del rival"},
        {"LessPowerCard","Eliminar la carta con menos poder del rival"},
        {"MultiplyPower", "Multiplica por n su ataque, siendo n la cantidad de cartas iguales a ella en el campo"},
        {"ClearRow", "Limpia la fila, no vacia, con menos unidades del rival"},
        {"Average", "Calcula el promedio de poder entre todas las carta del campo e iguala el poder de cada una a este promedio"},
    };
    public enum Factions
    {
        Gryffindor,
        Slytherin,
        Neutral
    }

    public enum AttackModes
    {
        Melee,
        Ranged,
        Siege
    }

    public static void HideImage(UnityEngine.UI.Image image)
    {
        image.color = new Color(image.color.r, image.color.g, image.color.b, 0);
    }

    public static void ShowImage(UnityEngine.UI.Image image)
    {
        image.color = new Color(image.color.r, image.color.g, image.color.b, 1);
    }

}
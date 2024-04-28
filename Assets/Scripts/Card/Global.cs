using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.UI;

public static class Global
{
    /// <summary>
    /// Efectos
    /// </summary>
    public static Dictionary<string, string> Effects = new Dictionary<string, string>
    {
        {"PutBoost", "Poner un aumento en una fila propia"},
        {"DrawCard", "Robar un carta"},
        {"PutWeather", "Poner un clima"},
        {"PowerfulCard", "Eliminar la carta con más poder del campo"},
        {"LessPowerCard","Eliminar la carta con menos poder del rival"},
        {"MultiplyPower", "Multiplica por n su ataque, siendo n la cantidad de cartas iguales a ella en el campo"},
        {"ClearRow", "Limpia la fila, no vacia, con menos unidades del rival"},
        {"Average", "Calcula el promedio de poder entre todas las carta del campo e iguala el poder de cada una a este promedio"},
    };

    /// <summary>
    /// Facciones
    /// </summary>
    public enum Factions
    {
        Gryffindor,
        Slytherin,
        Neutral
    }

    /// <summary>
    /// Formas de ataque, filas correspondientes
    /// </summary>
    public enum AttackModes
    {
        Melee,
        Ranged,
        Siege
    }
}
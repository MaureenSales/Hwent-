using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.UI;
#nullable enable

public static class Global
{
    public static Dictionary<string, Effect> EffectsCreated = new Dictionary<string, Effect>();
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
    public static Dictionary<string, string> SpecialsEffects = new Dictionary<string, string>
    {
        {"Decoy", "Coloca esta carta de poder 0 en el lugar de una carta en el campo para regresar esta a la mano"},
        {"WeatherMelee", "Iguala a 2 punto el valor de cada unidad cuerpo a cuerpo en el campo."},
        {"WeatherRanged", "Iguala a 2 punto el valor de cada unidad de ataque a distancia en el campo."},
        {"WeatherSiege", "Iguala a 2 punto el valor de cada unidad de asedio en el campo."},
        {"ClearWeather","Destruye los climas que afectan el campo"},
        {"Boost", " +2 Aumenta el poder de las unidades de una fila"},
        {"LeaderGryffindor", "Aumenta en el número de unidades cuerpo a cuerpo del rival cada unidad cuerpo a cuerpo propia"},
        {"LeaderSlytherin", "Robar dos cartas"},
    };

    public static List<string> TypeCards = new List<string>
    {
        "Oro",
        "Plata",
        "Lider",
        "Aumento",
        "Clima",
        "Despeje",
    };
    public static List<string> Sources = new List<string>
    {
        "board",
        "hand",
        "otherHand",
        "deck",
        "otherDeck",
        "boosterCells",
        "otherBoosterCells",
        "weatherZone",
        "field",
        "otherField",
        "parent",
    };

    /// <summary>
    /// Facciones
    /// </summary>
    public enum Factions
    {
        Gryffindor,
        Slytherin,
        Ravenclaw,
        Hufflepuff,
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

public class Skill
{
    public string Name { get; private set; }
    public Dictionary<string, object>? Arguments { get; private set; }
    public ASTnode? Selector { get; private set; }

    public Skill(string name, Dictionary<string, object>? argumennts, ASTnode? selector)
    {
        Name = name;
        Arguments = argumennts;
        Selector = selector;
    }
}
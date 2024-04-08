using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.UI;

public static class Global
{
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

    public enum Effects
    {
        //Weathers
        Fog, 
        Frost, 
        Rain,
        
        //Boost
        Boost,

        //Clear
        ClearSky, 

        //Decoy
        Dummy, 

        //Units
        IncreaseOwnRow,
        ApplyWeather,
        RemoveHighestPowerCardOwnField,
        RemoveHighestPowerCardOpponentField,
        RemoveLowestPowerCardOpponent,
        DrawCard,
        MultiplyByN,
        ClearRowFewestUnitsOwnFiel,
        ClearRowFewestUnitsOpponentField,
        EqualizeAllCardsPowerAverage,
        empty
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
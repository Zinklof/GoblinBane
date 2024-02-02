using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public int sensitivity;
    public float goblinVolume;
    public float towerVolume;
    public float abienceVolume;

    //defines what to start with when no save data is found or new save is made
    public GameData()
    {
        sensitivity = 1000;
        goblinVolume = 1.0f;
        towerVolume = 1.0f;
        abienceVolume = 1.0f;
    }
}

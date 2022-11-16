using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//issaugos zaidejo vieta ir health
//pasizek, issaugo bet nebeloadina
[System.Serializable]
public class PlayerData
{
    public int currentHealth;
    public float[] position;

    public PlayerData(character_movement player)
    {
        currentHealth = player.currentHealth;

        position = new float[3];
        position[0] = player.transform.position.x;
        position[1] = player.transform.position.y;
        position[2] = player.transform.position.z;
    }
}
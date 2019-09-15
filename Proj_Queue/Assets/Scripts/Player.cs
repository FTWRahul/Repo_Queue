using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    int health;
    public int Health { get { return health; } set { health = value; } }

    public void MakePlayer(PlayerData playerData)
    {
        health = playerData.health;

        var block = new MaterialPropertyBlock();
        block.SetColor("_BaseColor", playerData.color);
        GetComponent<Renderer>().SetPropertyBlock(block);
    }
}

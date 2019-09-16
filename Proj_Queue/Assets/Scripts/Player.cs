using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    int health;
    public int Health { get { return health; } set { health = value; } }
    public List<Vector2Int> movementPattern;

    public void MakePlayer(PlayerData playerData)
    {
        health = playerData.health;
        movementPattern = playerData.movementPattern;

        var block = new MaterialPropertyBlock();
        block.SetColor("_BaseColor", playerData.color);
        GetComponent<Renderer>().SetPropertyBlock(block);
    }
}

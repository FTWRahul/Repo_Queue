using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private int _health;
    public int Health => _health;
    public List<Vector2Int> movementPattern;
    
    private static readonly int BaseColor = Shader.PropertyToID("_BaseColor");

    public void MakePlayer(PlayerData playerData)
    {
        _health = playerData.health;
        movementPattern = playerData.movementPattern;

        var block = new MaterialPropertyBlock();
        block.SetColor(BaseColor, playerData.color);
        GetComponent<Renderer>().SetPropertyBlock(block);
    }
}

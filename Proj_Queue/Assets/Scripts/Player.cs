using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player
{
    [SerializeField]
    Cell currentCell;
    public Cell CurrentCell { get { return currentCell; } set { currentCell = value; } }

    [SerializeField]
    int health;
    [SerializeField]
    Color32 color;

    public Player(PlayerData playerData)
    {
        health = playerData.Health;
        color = playerData.Color;
    }
}

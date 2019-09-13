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
    public int Health { get { return health; } set { health = value; } }
    [SerializeField]
    Color32 color;
    public Color32 Color { get { return color; } set { color = value; } }

    public Player(PlayerData playerData)
    {
        health = playerData.Health;
        color = playerData.Color;
    }
}

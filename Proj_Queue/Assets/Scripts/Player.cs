using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMono : MonoBehaviour
{
    [SerializeField]
    PlayerData data;
    [SerializeField]
    int health;
    public int Health { get { return health; } set { health = value; } }
    [SerializeField]
    Color32 color;
    public Color32 Color { get { return color; } set { color = value; } }

    private void Start()
    {
        health = data.Health;
        GetComponent<MeshRenderer>().material.color = data.Color;
    }
}

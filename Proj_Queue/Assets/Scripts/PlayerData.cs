using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "PlayerData", order = 1)]
public class PlayerData : ScriptableObject
{
    [SerializeField]
    int health;
    public int Health {get {return health;}}
    [SerializeField]
    Color32 color;
    public Color32 Color { get { return color; }}
}

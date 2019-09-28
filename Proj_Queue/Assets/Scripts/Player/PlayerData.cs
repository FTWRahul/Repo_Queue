using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "PlayerData", menuName = "PlayerData", order = 1)]
public class PlayerData : ScriptableObject
{
    public int health;
    public Color32 color;
    public List<PatternData> movementPatterns;

    public List<CardData> originalDeck = new List<CardData>();
}

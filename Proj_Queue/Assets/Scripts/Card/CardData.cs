using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewCard", menuName = "Cards", order = 1)]
public class CardData : ScriptableObject
{
    public string cardName;
    public string cardDescription;
        
    public int energyCost;
    public int damage;
        
    public List<ActionData> actions;
}


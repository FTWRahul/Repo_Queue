using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newCard", menuName = "Card/ New card")]
public class CardData : ScriptableObject
{
    public string cardName;
    [TextArea]
    public string cardDescription;
        
    public int energyCost;
    public int damage;
        
    public List<ActionData> actions;
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewCard", menuName = "Card", order = 1)]
public class CardData : ScriptableObject
{
    public string cardName;
    public string cardDescription;
    
    public List<ActionData> actions = new List<ActionData>();
  
}

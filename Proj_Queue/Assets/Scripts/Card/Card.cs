using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Serialization;

[System.Serializable]
public class Card
{
    public string cardName; 
    public string cardDescription;

    public List<ActionData> actions;
    
    public Card(CardData cardData)
    {
        cardName = cardData.cardName;
        cardDescription = cardData.cardDescription;

        actions = cardData.actions;
    }
}
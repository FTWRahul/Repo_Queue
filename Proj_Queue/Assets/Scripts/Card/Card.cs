using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Card
{
    private readonly string _cardName;
    private readonly string _cardDescription;

    public List<ActionData> actions;
    
    public Card(CardData cardData)
    {
        _cardName = cardData.cardName;
        _cardDescription = cardData.cardDescription;

        actions = cardData.actions;
    }
    
}



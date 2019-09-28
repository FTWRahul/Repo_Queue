using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Card
{
    private readonly string _cardName;
    private readonly string _cardDescription;

    private readonly int _energyCost;
    private readonly int _damage;

    public List<ActionData> actions;
    
    public Card(CardData cardData)
    {
        _cardName = cardData.cardName;
        _cardDescription = cardData.cardDescription;
        _energyCost = cardData.energyCost;
        _damage = cardData.damage;

        actions = cardData.actions;
    }
    
}



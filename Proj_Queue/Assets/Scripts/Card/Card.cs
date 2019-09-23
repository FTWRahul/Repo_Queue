using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card
{
    public readonly string cardName;
    public readonly string cardDescription;
        
    public readonly int energyCost;
    public readonly int damage;

    public List<ActionData> actions;
    
    public Card(CardData cardData)
    {
        cardName = cardData.cardName;
        cardDescription = cardData.cardDescription;
        energyCost = cardData.energyCost;
        damage = cardData.damage;

        actions = cardData.actions;
    }
    
}



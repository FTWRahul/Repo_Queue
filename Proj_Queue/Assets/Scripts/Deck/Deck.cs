using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour
{
    protected List<Card> deck = new List<Card>();
    
    public void MoveCard(int atIndex, Deck toDeck)
    {
        toDeck.deck.Add(deck[atIndex]);
        deck.RemoveAt(atIndex);
    }
}

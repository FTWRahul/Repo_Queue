using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Static;

public class DeckManager : MonoBehaviour
{
    public List<Card> originalDeck = new List<Card>();
    public Stack<Card> deck = new Stack<Card>();
    public List<Card> hand = new List<Card>();


    public Transform handPanel;
    
    
    private void Start()
    {
        
        ResetDeck();
    }

    void ResetDeck()
    {
        originalDeck.Shuffle();

        foreach (Card card in originalDeck)
        {
            deck.Push(card);
        }
    }

    void DealCard()
    {
        if (deck.Count <= 0)
        {
            ResetDeck();
        }
        
        hand.Add(deck.Pop());
    }

    public void SetDeck(List<Card> cards)
    {
        originalDeck = cards;
    }
}


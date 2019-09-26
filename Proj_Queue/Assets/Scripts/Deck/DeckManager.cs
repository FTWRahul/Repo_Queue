using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Static;
using UnityEngine.Serialization;

public class DeckManager : MonoBehaviour
{ 
    public List<Card> originalDeckList = new List<Card>();
    public Stack<Card> deck = new Stack<Card>();
    public List<Card> hand = new List<Card>();

    private void Start()
    {
        ResetDeck();
    }

    void ResetDeck()
    {
        originalDeckList.Shuffle();

        foreach (Card card in originalDeckList)
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
        
        Card newCard = Instantiate(deck.Pop());

        foreach (Card card in hand)
        {
            if (card.cardName == newCard.cardName)
            {
                return;
            }
        }
        
        hand.Add(newCard);
        
    }
}


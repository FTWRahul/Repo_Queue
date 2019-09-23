using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawDeck : Deck
{
   public List<Card> ShuffleDeck()
   {
      List<Card> newDeck = new List<Card>(deck.Count);
      for (int i = 0; i < deck.Count; i++)
      {
         int rand = Random.Range(0, deck.Count);
         newDeck.Add(deck[rand]);
         deck.RemoveAt(rand);
      }

      return newDeck;
   }
}

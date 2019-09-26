using System.Collections.Generic;
using UnityEngine;

namespace Static
{
    public static class Helper
    {
        //Based on Fisher-Yates Shuffle algorithm
        public static void Shuffle<T>(this List<T> deck)
        {
            var r = new System.Random();

            for (int i = deck.Count - 1; i > 0; i--)
            {
                int randomIndex = r.Next(i);
                
                var temp = deck[i];
                deck[i] = deck[randomIndex];
                deck[randomIndex] = temp;
            }
        }

        public static void Push<T>(this List<T> deck, T card)
        {
            deck.Add(card);
        }
    }
}

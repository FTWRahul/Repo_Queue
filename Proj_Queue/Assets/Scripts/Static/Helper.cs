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

            for (int i = deck.Count - 1; i > 0; i++)
            {
                int k = r.Next(i + 1);
                
                var temp = deck[i];
                deck[i] = deck[k];
                deck[k] = temp;
            }
        }

        public static void Push<T>(this List<T> deck, Card card)
        {
            
        }
        
        public static Card Pop<T>(this List<T> deck)
        {
            return null;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
    
    public void ReceiveSelectedCell(Vector2Int cellPos)
    {
        foreach (var action in actions)
        {
            action.originCell = cellPos;
        }
    }
    
    public void DisplayAction()
    {
        foreach (var action in actions)
        {
            action.DisplayPossiblePattern(Board.boardInstance.GetPlayerPosition(Board.boardInstance.CurrentPlayer));
        }
    }
}



using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardTestRahul : MonoBehaviour
{
    public CardData carddata;

    public Card card;
    // Start is called before the first frame update
    void Start()
    {
        card = new Card(carddata);
    }

    [ContextMenu("Display Pattern Now")]
    public void DisplayPattern()
    {
        
        card.actions[0].DisplayPossiblePattern(Board.boardInstance.GetPlayerPosition(FindObjectOfType<GameManager>().CurrentPlayer));
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandArea : DropArea
{
    public int limit;
    
    //TODO:: need to call this method after a card has been receive cell position
    public void EnableDraggable()
    {
        foreach (Transform card in this.transform)
        {
            if (card.GetComponent<Draggable>())
            {
                card.GetComponent<Draggable>().enabled = true;
            }
        }
    }
    
    public void DisableDraggable()
    {
        foreach (Transform card in this.transform)
        {
            if (card.GetComponent<Draggable>())
            {
                card.GetComponent<Draggable>().enabled = false;
            }
        }
    }
}

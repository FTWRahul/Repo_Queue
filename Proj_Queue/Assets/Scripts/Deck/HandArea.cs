using UnityEngine;

public class HandArea : DropArea
{
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

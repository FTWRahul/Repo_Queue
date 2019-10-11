using UnityEngine;
using UnityEngine.EventSystems;

public class DiscardArea : DropArea
{
    protected override void SetParent(PointerEventData eventData)
    {
        Debug.Log("Here");
        Draggable draggable = eventData.pointerDrag.GetComponent<Draggable>();
        
        if (draggable != null)
        {
            Destroy(draggable.gameObject);
        }
    }
}

using UnityEngine;
using UnityEngine.EventSystems;

public class DropArea : MonoBehaviour , IDropHandler , IPointerEnterHandler, IPointerExitHandler
{
    void IDropHandler.OnDrop(PointerEventData eventData)
    {
        SetParent(eventData);
    }

    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
    {

    }

    void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
    {

    }

    protected virtual void SetParent(PointerEventData eventData)
    {
        Draggable draggable = eventData.pointerDrag.GetComponent<Draggable>();
        
        if (draggable != null)
        {
            draggable.SetDropArea(transform);
        }
    }
}

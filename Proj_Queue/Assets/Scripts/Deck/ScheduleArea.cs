using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ScheduleArea : DropArea
{
    public int limit = 3;

    protected override void SetParent(PointerEventData eventData)
    {
        if(transform.childCount >= limit) return;
        
        base.SetParent(eventData);
    }
}

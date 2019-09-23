using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Draggable : MonoBehaviour, IBeginDragHandler,IDragHandler, IEndDragHandler
{
    private Transform _areaToDrop = null;
    private CanvasGroup _canvasGroup;

    [SerializeField] private GameObject placeHolder;

    private void Start()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
    }

    void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
    {
        _canvasGroup.blocksRaycasts = false;

/*        placeHolder = new GameObject();
        placeHolder.transform.SetParent(transform.parent);
        placeHolder.transform.SetSiblingIndex(transform.GetSiblingIndex());
        */

        _areaToDrop = transform.parent;
        transform.SetParent(_areaToDrop.parent);
    }
    
    void IDragHandler.OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
        
        /*
        for (int i = 0; i < _areaToDrop.childCount; i++)
        {
            if (transform.position.x < _areaToDrop.GetChild(i).position.x)
            {
                placeHolder.transform.SetSiblingIndex(i);
            }
        }*/
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        _canvasGroup.blocksRaycasts = true;
        transform.SetParent(_areaToDrop);
        /*
        transform.SetSiblingIndex(placeHolder.transform.GetSiblingIndex());
        */
    }

    public void SetDropArea(Transform area)
    {
        _areaToDrop = area;
        transform.rotation = area.transform.rotation;
    }
}

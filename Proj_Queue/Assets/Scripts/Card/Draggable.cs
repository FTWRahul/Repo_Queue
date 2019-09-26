using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

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
        placeHolder = new GameObject();
        placeHolder.transform.SetParent(transform.parent);
        
        LayoutElement layoutElement = placeHolder.AddComponent<LayoutElement>();
        layoutElement.preferredHeight = 100;
        layoutElement.preferredWidth = 100;
        
        placeHolder.transform.SetSiblingIndex(transform.GetSiblingIndex());
        
        _areaToDrop = transform.parent;
        transform.SetParent(_areaToDrop.parent);
        
        _canvasGroup.blocksRaycasts = false;
    }
    
    void IDragHandler.OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
        
        for (int i = 0; i < _areaToDrop.childCount; i++)
        {
            if (transform.position.x < _areaToDrop.GetChild(i).position.x)
            {
                placeHolder.transform.SetSiblingIndex(i);
                break;
            }
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.SetParent(_areaToDrop);
        transform.SetSiblingIndex(placeHolder.transform.GetSiblingIndex());
        
        _canvasGroup.blocksRaycasts = true;
        Destroy(placeHolder);
    }

    public void SetDropArea(Transform area)
    {
        _areaToDrop = area;
        transform.rotation = area.transform.rotation;
    }
}

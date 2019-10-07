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
    private CardDisplayer _cardDisplayer;

    [SerializeField] private GameObject placeHolder;

    private void Start()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
        _cardDisplayer = GetComponent<CardDisplayer>();
    }

    void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
    {
        placeHolder = new GameObject();
        Transform parent = transform.parent;
            
        placeHolder.transform.SetParent(parent);
        
        LayoutElement layoutElement = placeHolder.AddComponent<LayoutElement>();
        layoutElement.preferredHeight = 100;
        layoutElement.preferredWidth = 100;
        
        placeHolder.transform.SetSiblingIndex(transform.GetSiblingIndex());
        
        _areaToDrop = parent;
        transform.SetParent(_areaToDrop.parent);
        
        _canvasGroup.blocksRaycasts = false;
        
        Board.boardInstance.gameManager.OnCardDragEvent();
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
        _canvasGroup.blocksRaycasts = true;

        Destroy(placeHolder);
        
        if (_areaToDrop.GetComponent<ScheduleArea>())
        {
            _cardDisplayer.CardInfo.DisplayAction();
            
            Board.boardInstance.gameManager.OnCardDropEvent();
            Board.boardInstance.gameManager.ReceiveSelectedCellEvent += _cardDisplayer.CardInfo.ReceiveSelectedCell;
            this.enabled = false;
        }
    }

    public void SetDropArea(Transform area)
    {
        _areaToDrop = area;
        transform.rotation = area.transform.rotation;
    }
}

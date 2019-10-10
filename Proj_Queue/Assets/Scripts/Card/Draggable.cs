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
    
    private GameObject _placeHolder;

    private void Start()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
        _cardDisplayer = GetComponent<CardDisplayer>();
    }

    void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
    {
        //Adding a placeholder
        _placeHolder = new GameObject();
        Transform parent = transform.parent;
            
        _placeHolder.transform.SetParent(parent);
        
        LayoutElement layoutElement = _placeHolder.AddComponent<LayoutElement>();
        layoutElement.preferredHeight = 100;
        layoutElement.preferredWidth = 100;
        
        _placeHolder.transform.SetSiblingIndex(transform.GetSiblingIndex());
        
        //Set returning parent
        _areaToDrop = parent;
        transform.SetParent(_areaToDrop.parent);
        _canvasGroup.blocksRaycasts = false;
        
        //Calling OnCardDragEvent on game manager??
        Board.boardInstance.gameManager.OnCardDragEvent();
    }
    
    void IDragHandler.OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
        
        for (int i = 0; i < _areaToDrop.childCount; i++)
        {
            if (!(transform.position.x < _areaToDrop.GetChild(i).position.x)) continue;
            
            _placeHolder.transform.SetSiblingIndex(i);
            break;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        //Setting new parent for returning
        transform.SetParent(_areaToDrop);
        _canvasGroup.blocksRaycasts = true;

        //Destroying the placeholder
        Destroy(_placeHolder);
        
        if (!_areaToDrop.GetComponent<ScheduleArea>()) return;
        
/*        _cardDisplayer.CardInfo.DisplayAction();*/
            
        Board.boardInstance.gameManager.OnCardDropEvent();
/*        Board.boardInstance.gameManager.ReceiveSelectedCellEvent += _cardDisplayer.CardInfo.ReceiveSelectedCell;*/
        this.enabled = false;
    }

    public void SetDropArea(Transform area)
    {
        _areaToDrop = area;
        transform.rotation = area.transform.rotation;
    }
}

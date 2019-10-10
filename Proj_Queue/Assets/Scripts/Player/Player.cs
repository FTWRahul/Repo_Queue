using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Player : MonoBehaviour
{
    public List<PatternData> MovementPatterns { get; private set; }

    private List<Card> _originalDeck = new List<Card>();
    
    private Renderer _renderer;
    private DeckManager _deckManager;
    [SerializeField] private GameObject canvas;
    
    public delegate void OnEndTurnDelegate();
    public event OnEndTurnDelegate EndPlayerTurnEvent = delegate { };
    
    public delegate void OnStartTurnDelegate();
    public event OnStartTurnDelegate StartPlayerTurnEvent = delegate { };

    public delegate void OnCardDropDelegate();
    public event OnCardDropDelegate CardDropEvent = delegate { };
    
    public delegate void OnCardDragDelegate();
    public event OnCardDragDelegate CardDragEvent = delegate { };
    
    public void Awake()
    {
        _deckManager = GetComponent<DeckManager>();
        _renderer = GetComponent<Renderer>();
        StartPlayerTurnEvent += HighlightMovementCells;
        StartPlayerTurnEvent += _deckManager.DealCard;
        EndPlayerTurnEvent += _deckManager.EndTurn;
/*        CardDropEvent += _deckManager.handPanel.GetComponent<HandArea>().DisableDraggable;*/
        
    }

    public void MakePlayer(PlayerData playerData)
    {
        MovementPatterns = playerData.movementPatterns;
        foreach (CardData cardData in playerData.originalDeck)
        {
            _originalDeck.Add(new Card(cardData));
        }
        _deckManager.InitDeck(_originalDeck);
        
        _renderer.material.color = playerData.color;
        canvas.SetActive(false);
    }
    
    public void Move(Vector2Int cellPos)
    {
        //Changing reference on board
        Board.boardInstance.ChangePlayerPos(gameObject, cellPos);
        //Physically lerping the player
        transform.DOMove(Board.boardInstance.CellLayer[cellPos.x, cellPos.y].transform.position + Vector3.up, 2f).SetEase(Ease.OutQuint);
    }
    
    private void HighlightMovementCells()
    {
        Vector2Int thisPosition = Board.boardInstance.GetPlayerPosition(gameObject);
        
        foreach (PatternData pattern in MovementPatterns)
        {
            foreach (Vector2Int pos in pattern.positions)
            {
                Vector2Int resultingPos = thisPosition + pos;
                
                if (resultingPos.x < 0 || resultingPos.x > Board.boardInstance.Width - 1 || resultingPos.y < 0 || resultingPos.y > Board.boardInstance.Height - 1) // outside of the board
                {
                    break;
                }

                if (Board.boardInstance.PlayerLayer[resultingPos.x, resultingPos.y] != null) // player is on cell
                {
                    break;
                }
                
                Board.boardInstance.CellLayer[resultingPos.x, resultingPos.y].Highlight();
            }
        }
    }
    
    
    public void OnStartPlayerTurnEvent()
    {
        Debug.Log("here");
        StartPlayerTurnEvent?.Invoke();
        canvas.SetActive(true);
    }
    
    public void OnEndPlayerTurnEvent()
    {
        EndPlayerTurnEvent?.Invoke();
        canvas.SetActive(false);
    }

    public void OnCardDropEvent()
    {
        CardDropEvent?.Invoke();
    }


    protected virtual void OnCardDragEvent()
    {
        CardDragEvent?.Invoke();
    }
}

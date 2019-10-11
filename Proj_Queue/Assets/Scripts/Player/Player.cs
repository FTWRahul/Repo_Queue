using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Player : MonoBehaviour
{
    private List<PatternData> MovementPatterns { get; set; }

    private List<Card> _originalDeck = new List<Card>();
    
    private Renderer _renderer;
    private DeckManager _deckManager;
    [SerializeField] private GameObject canvas;
    [SerializeField] private GameObject endTurnButton;
    
    public delegate void OnEndTurnDelegate();
    public event OnEndTurnDelegate EndPlayerTurnEvent = delegate { };
    
    public delegate void OnStartTurnDelegate();
    public event OnStartTurnDelegate StartPlayerTurnEvent = delegate { };

    public delegate void OnCardDropDelegate();
    public event OnCardDropDelegate CardDropEvent = delegate { };
    
    public delegate void OnCardReceivedCellDelegate();
    public event OnCardReceivedCellDelegate CardReceivedCellEvent = delegate { };
    
    public void Awake()
    {
        _deckManager = GetComponent<DeckManager>();
        _renderer = GetComponent<Renderer>();
        
        //Subscribing to all delegates
        StartPlayerTurnEvent += HighlightMovementCells;
        StartPlayerTurnEvent += _deckManager.DealCard;
        
        EndPlayerTurnEvent += _deckManager.EndTurn;
        
        CardDropEvent += _deckManager.OnCardDropEvent;
        CardReceivedCellEvent += _deckManager.OnCardReceivedCellEvent;
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
        Board.BoardInstance.ChangePlayerPos(gameObject, cellPos);
        //Physically lerping the player
        transform.DOMove(Board.BoardInstance.CellLayer[cellPos.x, cellPos.y].transform.position + Vector3.up, 2f).SetEase(Ease.OutQuint);
    }
    
    private void HighlightMovementCells()
    {
        Vector2Int thisPosition = Board.BoardInstance.GetPlayerPosition(gameObject);
        
        foreach (PatternData pattern in MovementPatterns)
        {
            foreach (Vector2Int pos in pattern.positions)
            {
                Vector2Int resultingPos = thisPosition + pos;
                
                if (resultingPos.x < 0 || resultingPos.x > Board.BoardInstance.Width - 1 || resultingPos.y < 0 || resultingPos.y > Board.BoardInstance.Height - 1) // outside of the board
                {
                    break;
                }

                if (Board.BoardInstance.PlayerLayer[resultingPos.x, resultingPos.y] != null) // player is on cell
                {
                    break;
                }
                
                Board.BoardInstance.CellLayer[resultingPos.x, resultingPos.y].Highlight();
            }
        }
    }
    
    
    public void OnStartPlayerTurnEvent()
    {
        //Calling start player turn event: DeckManager.DealCard
        StartPlayerTurnEvent?.Invoke();
        //Enabling canvas
        canvas.SetActive(true);
    }
    
    public void OnEndPlayerTurnEvent()
    {
        //Calling end turn event: deckManager.EndTurn
        EndPlayerTurnEvent?.Invoke();
    }

    public void DisableCanvas()
    {
        //Disabling canvas
        canvas.SetActive(false);
    }

    public void OnCardDropEvent()
    {
        //Calling card drop event: deck.Manager.OnCardDrop => handArea.DisableDraggable
        CardDropEvent?.Invoke();
        endTurnButton.SetActive(false);
    }


    public void OnCardReceivedCellEvent()
    {
        //Calling card drop event: deck.Manager.OnCardReceivedCell => handArea.EnableDraggable
        CardReceivedCellEvent?.Invoke();
        endTurnButton.SetActive(true);
    }
}

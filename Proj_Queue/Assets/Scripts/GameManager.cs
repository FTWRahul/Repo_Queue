using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    private Board _board;
    private CellSelector _cellSelector;
    
    [SerializeField] private BoardData boardData;
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private List<PlayerData> playersData = new List<PlayerData>();
    [SerializeField] private List<Vector2Int> playersStartingPositions = new List<Vector2Int>();
    [SerializeField] private List<Player> players = new List<Player>();
    [SerializeField] private int currentPlayerIndex;

    //On start turn delegate
    public delegate void OnStartTurnDelegate();
    public event OnStartTurnDelegate StartTurnEvent = delegate { };
    
    //On End turn delegate
    public delegate void OnEndTurnDelegate();
    public event OnEndTurnDelegate EndTurnEvent = delegate { };

    //On cell selected delegate
    public delegate void OnCellSelectorDelegate(Vector2Int cellPos);
    public event OnCellSelectorDelegate ReceiveSelectedCellEvent = delegate { };

    //On card drag delegate
    public delegate void OnCardDragDelegate();
    public event OnCardDragDelegate CardDragEvent = delegate { };
    
    //On card drop delegate
    public delegate void OnCardDropDelegate();
    public event OnCardDropDelegate CardDropEvent = delegate { };

    //On card scheduled delegate
    public delegate void OnCardReceivedCellDelegate();
    public event OnCardReceivedCellDelegate CardReceivedCellEvent = delegate { };

    void Start()
    {
        //Ref and making the board
        _board = FindObjectOfType<Board>();
        _board.MakeBoard(boardData);
        
        //Initializing the players 
        InitPlayers();
        
        //Ref cell selector
        _cellSelector = FindObjectOfType<CellSelector>();
        
        //Subscribing ReceiveHitCellEvent method to CellHitEvent event
        _cellSelector.CellHitEvent += ReceiveHitCellEvent;
        
        //Subscribing boardhighlighter to events
        EndTurnEvent += _board.BoardHighlighter.DehighlightCells;
        CardDragEvent += _board.BoardHighlighter.DehighlightCells;
        
        OnStartTurnEvent();
    }

    void OnStartTurnEvent()
    {
        //Call all methods which subscribed to StartTurnEvent, if not null
        StartTurnEvent?.Invoke();
    }

    
    public GameObject GetCurrentPlayerGameObject()
    {
        //Return current player game object
        return players[currentPlayerIndex].gameObject;
    }

    private void InitPlayers()
    {
        for (int i = 0; i < playersData.Count; i++)
        {
            Player go = Instantiate(playerPrefab, Vector3.zero, Quaternion.identity).GetComponent<Player>();
            //Adding player to players list
            players.Add(go);
            //Setting player's canvas inactive

            go.MakePlayer(playersData[i]);
            _board.PlacePlayer(go.gameObject, playersStartingPositions[i]);
        }
        currentPlayerIndex = 0;
        SubscribeCurrentPlayerToDelegates();
    }

    public void OnEndPlayerTurnEvent()
    {
        EndTurnEvent?.Invoke();
        
        //Calling it through Invoke to simulate coroutine
        Invoke("EndPlayerTurn", 1 );
    }
    
    private void EndPlayerTurn()
    {
        //Unsubscribe current player methods from delegates 
        UnsubscribeCurrentPlayerFromDelegate();
        
        //Changing current player index
        if (currentPlayerIndex < players.Count-1)
        {
            currentPlayerIndex++;
        }
        else
        {
            currentPlayerIndex = 0;
        }

        //Subscribe current player methods to delegates
        SubscribeCurrentPlayerToDelegates();

        //Calling start turn event
        OnStartTurnEvent();
    }

    private void ReceiveHitCellEvent(Vector2Int cellPos)
    {
        //Call all methods which subscribed to ReceiveSelectedCellEvent, if not null
        ReceiveSelectedCellEvent?.Invoke(cellPos);
        
        //TODO:: call this method with delegate???
        _board.BoardHighlighter.DehighlightCells();
    }
    
    private void SubscribeCurrentPlayerToDelegates()
    {
        //Subscribing current player methods to delegates
        // when selected cell will be received MOVE method on current player will called
        ReceiveSelectedCellEvent += players[currentPlayerIndex].Move;
        // On start turn event - current player OnStartPlayerTurnEvent will be called
        StartTurnEvent += players[currentPlayerIndex].OnStartPlayerTurnEvent;
        // On end turn event  - current player endPlayerTurnEvent called
        EndTurnEvent += players[currentPlayerIndex].OnEndPlayerTurnEvent;
        // On card drop event - current player CardDropEvent will be called
        CardDropEvent += players[currentPlayerIndex].OnCardDropEvent;
        //Card received cell event - current player card received cell will be called
        CardReceivedCellEvent += players[currentPlayerIndex].OnCardReceivedCellEvent;
    }
    
    private void UnsubscribeCurrentPlayerFromDelegate()
    {
        ReceiveSelectedCellEvent -= players[currentPlayerIndex].Move;
        StartTurnEvent -= players[currentPlayerIndex].OnStartPlayerTurnEvent;
        EndTurnEvent -= players[currentPlayerIndex].OnEndPlayerTurnEvent;
        CardDropEvent -= players[currentPlayerIndex].OnCardDropEvent;
        CardReceivedCellEvent -= players[currentPlayerIndex].OnCardReceivedCellEvent;
    }

    public void OnCardDragEvent()
    {
        //Call all methods which subscribed to CardDragEvent, if not null
        CardDragEvent?.Invoke();
    }

    public void OnCardDropEvent()
    {
        //Unsubscribe current player from ReceiveSelectedCellEvent
        ReceiveSelectedCellEvent -= players[currentPlayerIndex].Move;
        
        //Call all methods which subscribed to CardDropEvent, if not null
        CardDropEvent?.Invoke();
    }

    public void OnCardReceiveCellEvent()
    {
        CardReceivedCellEvent?.Invoke();
    }
}

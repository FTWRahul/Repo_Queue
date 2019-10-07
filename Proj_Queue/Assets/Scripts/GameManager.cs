using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Board board;
    [SerializeField] private BoardData boardData;
    
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private List<PlayerData> playersData = new List<PlayerData>();
    [SerializeField] private List<Vector2Int> playersStartingPositions = new List<Vector2Int>();
    [SerializeField] private List<Player> players = new List<Player>();
    [SerializeField] private int currentPlayerIndex;

    private CellSelector _cellSelector;
    
    public delegate void OnCellSelectorDelegate(Vector2Int cellPos);
    public event OnCellSelectorDelegate ReceiveSelectedCellEvent = delegate { };

    public delegate void OnEndTurnDelegate();
    public event OnEndTurnDelegate EndTurnEvent = delegate { };

    public delegate void OnStartTurnDelegate();
    public event OnStartTurnDelegate StartTurnEvent = delegate { };
    
    public delegate void OnCardDragDelegate();
    public event OnCardDragDelegate CardDragEvent = delegate { };
    
    public delegate void OnCardDropDelegate();
    public event OnCardDropDelegate CardDropEvent = delegate { };


    void Start()
    {
        board.MakeBoard(boardData);
        InitPlayers();
        
        _cellSelector = FindObjectOfType<CellSelector>();
        _cellSelector.OnCellSelectedEvent += OnCellSelected;

        StartTurnEvent();
    }

    public GameObject GetCurrentPlayerGameObject()
    {
        return players[currentPlayerIndex].gameObject;
    }

    private void InitPlayers()
    {
        for (int i = 0; i < playersData.Count; i++)
        {
            Player go = Instantiate(playerPrefab, Vector3.zero, Quaternion.identity).GetComponent<Player>();
            players.Add(go);
            go.canvas.SetActive(false);
            
            go.MakePlayer(playersData[i]);
            board.PlacePlayer(go.gameObject, playersStartingPositions[i]);

            if (i != 0) continue;
            
            currentPlayerIndex = i;
            go.canvas.SetActive(true);
            ReceiveSelectedCellEvent += go.Move;
            EndTurnEvent += go.OnEndPlayerTurnEvent;
            StartTurnEvent += go.HighlightMovementCells;
        }
    }

    public void OnEndPlayerTurnEvent()
    {
        EndTurnEvent();
        Invoke("EndPlayerTurn", 2 );
    }
    
    private void EndPlayerTurn()
    {
        players[currentPlayerIndex].canvas.SetActive(false);
        UnsubscribeDelegate();
        StartTurnEvent -= players[currentPlayerIndex].HighlightMovementCells;
        EndTurnEvent -= players[currentPlayerIndex].OnEndPlayerTurnEvent;
        
        if (currentPlayerIndex < players.Count-1)
        {
            currentPlayerIndex++;
        }
        else
        {
            currentPlayerIndex = 0;
        }
        
        players[currentPlayerIndex].canvas.SetActive(true);
        ReceiveSelectedCellEvent += players[currentPlayerIndex].Move;
        StartTurnEvent += players[currentPlayerIndex].HighlightMovementCells;
        EndTurnEvent += players[currentPlayerIndex].OnEndPlayerTurnEvent;
        
        StartTurnEvent();
    }

    void OnCellSelected(Vector2Int cellPos)
    {
        ReceiveSelectedCellEvent(cellPos);
        //TODO:: call this method with delegate???
        board.BoardHighlighter.DehighlightCells();
    }

    private void UnsubscribeDelegate()
    {
        ReceiveSelectedCellEvent -= players[currentPlayerIndex].Move;
    }

    public void OnCardDragEvent()
    {
        CardDragEvent();
    }

    public void OnCardDropEvent()
    {
        UnsubscribeDelegate();
        CardDropEvent();
    }
}

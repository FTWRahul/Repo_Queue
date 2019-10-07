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
    [SerializeField] private List<Player> players = new List<Player>();
    [SerializeField] private List<Vector2Int> playersStartingPositions = new List<Vector2Int>();
    [SerializeField] private int currentPlayerIndex;

    private CellSelector _cellSelector;
    
    public delegate void OnCellSelectorDelegate(Vector2Int cellPos);
    public event OnCellSelectorDelegate OnReceiveSelectedCellEvent = delegate { };

    public delegate void OnEndTurnDelegate();
    public event OnEndTurnDelegate OnEndTurnEvent = delegate { };

    public delegate void OnStartTurnDelegate();
    public event OnStartTurnDelegate OnStartTurnEvent = delegate { };

    void Start()
    {
        board.MakeBoard(boardData);
        InitPlayers();
        
        _cellSelector = FindObjectOfType<CellSelector>();
        _cellSelector.OnCellSelectedEvent += OnCellSelected;

        OnStartTurnEvent();
    }

    public GameObject GetCurrentPlayerGameObject()
    {
        return players[currentPlayerIndex].gameObject;
    }

    void InitPlayers()
    {
        for (int i = 0; i < playersData.Count; i++)
        {
            Player go = Instantiate(playerPrefab, Vector3.zero, Quaternion.identity).GetComponent<Player>();
            players.Add(go);
            if (i == 0)
            {
                currentPlayerIndex = i;
                OnReceiveSelectedCellEvent += go.Move;
                OnStartTurnEvent += go.HighlightMovementCells;
            }
            else
            {
                go.canvas.SetActive(false);
            }
            go.MakePlayer(playersData[i]);
            board.PlacePlayer(go.gameObject, playersStartingPositions[i]);
        }
    }
    
    public void EndPlayerTurn()
    {
        OnEndTurnEvent();
        
        players[currentPlayerIndex].canvas.SetActive(false);
        UnsubscribeDelegate();
        OnStartTurnEvent -= players[currentPlayerIndex].HighlightMovementCells;
        
        if (currentPlayerIndex < players.Count-1)
        {
            currentPlayerIndex++;
        }
        else
        {
            currentPlayerIndex = 0;
        }
        
        players[currentPlayerIndex].canvas.SetActive(true);
        OnReceiveSelectedCellEvent += players[currentPlayerIndex].Move;
        OnStartTurnEvent += players[currentPlayerIndex].HighlightMovementCells;

        OnStartTurnEvent();
    }

    void OnCellSelected(Vector2Int cellPos)
    {
        OnReceiveSelectedCellEvent(cellPos);
        //TODO:: call this method with delegate???
        board.BoardHighlighter.DehighlightCells();
    }

    public void UnsubscribeDelegate()
    {
        OnReceiveSelectedCellEvent -= players[currentPlayerIndex].Move;
    }
}

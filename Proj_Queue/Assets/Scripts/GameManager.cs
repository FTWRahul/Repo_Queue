using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Board board;
    [SerializeField] private BoardData boardData;
    
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private PlayerData playerData;
    [SerializeField] private PlayerData player1Data;

    [field: SerializeField] public Player CurrentPlayer { get; private set; }
    [SerializeField] private Player otherPlayer;

    private CellSelector _cellSelector;
    
    public delegate void OnCellSelectorDelegate(Vector2Int cellPos);
    public event OnCellSelectorDelegate OnReceiveSelectedCellEvent = delegate { };
    
    
    void Start()
    {
        board.MakeBoard(boardData);

        Player go = Instantiate(playerPrefab, Vector3.zero, Quaternion.identity).GetComponent<Player>();
        CurrentPlayer = go;
        go.MakePlayer(playerData);
        go.canvas.SetActive(true);
        board.PlacePlayer(go.gameObject, new Vector2Int(2, 5));
        OnReceiveSelectedCellEvent += CurrentPlayer.Move;
        
        Player go1 = Instantiate(playerPrefab, Vector3.zero, Quaternion.identity).GetComponent<Player>();
        otherPlayer = go1;
        go1.MakePlayer(player1Data);
        go1.canvas.SetActive(false);
        board.PlacePlayer(go1.gameObject, new Vector2Int(2, 0));


        _cellSelector = FindObjectOfType<CellSelector>();
        _cellSelector.OnCellSelectedEvent += OnCellSelected;
        
        board.BoardHighlighter.HighlightMovementCells(CurrentPlayer.gameObject);
    }
    
    /// <summary>
    /// Changes the current player and removes all the previous possible move space
    /// </summary>
    [ContextMenu("EndTurn")]
    public void EndPlayerTurn()
    {
        Player temp = CurrentPlayer;
        CurrentPlayer = otherPlayer;
        CurrentPlayer.canvas.SetActive(true);
        OnReceiveSelectedCellEvent += CurrentPlayer.Move;
        otherPlayer = temp;
        otherPlayer.canvas.SetActive(false);
        OnReceiveSelectedCellEvent -= otherPlayer.Move;
        
        board.BoardHighlighter.DehighlightCells();
        
        board.BoardHighlighter.HighlightMovementCells(CurrentPlayer.gameObject);
    }

    void OnCellSelected(Vector2Int cellPos)
    {
        OnReceiveSelectedCellEvent(cellPos);
    }
    
}

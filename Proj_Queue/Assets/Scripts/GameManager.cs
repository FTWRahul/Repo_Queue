﻿using System;
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

    [field: SerializeField] public GameObject CurrentPlayer { get; private set; }
    [SerializeField] private GameObject otherPlayer;
    
    void Start()
    {
        board.MakeBoard(boardData);

        GameObject go = Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);

        CurrentPlayer = go;

        Player player = go.GetComponent<Player>();

        player.MakePlayer(playerData);
        
        player.canvas.SetActive(false);
        
        board.PlacePlayer(player.gameObject, new Vector2Int(2, 5));



        GameObject go1 = Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);

        otherPlayer = go1;

        Player player1 = go1.GetComponent<Player>();

        player1.MakePlayer(player1Data);
        
        player1.canvas.SetActive(true);

        board.PlacePlayer(player1.gameObject, new Vector2Int(2, 0));

        //EndPlayerTurn();
        //board.BoardHighlighter.HighlightMovementCells(CurrentPlayer);
        //Turn(CurrentPlayer);
    }
    
    /// <summary>
    /// Changes the current player and removes all the previous possible move space
    /// </summary>
    [ContextMenu("EndTurn")]
    public void EndPlayerTurn()
    {
        GameObject temp = CurrentPlayer;
        CurrentPlayer = otherPlayer;
        CurrentPlayer.GetComponent<Player>().canvas.SetActive(true);
        otherPlayer = temp;
        otherPlayer.GetComponent<Player>().canvas.SetActive(false);
        
        board.BoardHighlighter.DehighlightCells();
        
        board.BoardHighlighter.HighlightMovementCells(CurrentPlayer);
    }

    private void Turn(GameObject player)
    {
        board.BoardHighlighter.HighlightMovementCells(CurrentPlayer.gameObject);
    }
}

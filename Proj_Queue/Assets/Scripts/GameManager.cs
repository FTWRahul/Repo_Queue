using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Board board;
    public BoardData boardData;

    public GameObject playerPrefab;
    public PlayerData playerData;
    public PlayerData player1Data;

    public GameObject currentPlayer;
    public GameObject otherPlayer;

    void Start()
    {
        board.MakeBoard(boardData);

        GameObject go = Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);

        currentPlayer = go;

        Player player = go.GetComponent<Player>();

        player.MakePlayer(playerData);
        
        board.PlacePlayer(player.gameObject, new Vector2Int(2, 5));



        GameObject go1 = Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);

        otherPlayer = go1;

        Player player1 = go1.GetComponent<Player>();

        player1.MakePlayer(player1Data);

        board.PlacePlayer(player1.gameObject, new Vector2Int(2, 0));

        Turn(currentPlayer);
    }

    public void MakeMove(Vector2Int cellPos)
    {
        board.MovePlayer(currentPlayer, cellPos);
        board.DehighlightCells();

        GameObject temp = currentPlayer;
        currentPlayer = otherPlayer;
        otherPlayer = temp;

        Turn(currentPlayer);
    }

    public void Turn(GameObject player)
    {
        board.HighlightMovementCells(player.gameObject);
    }
}

﻿using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    public GameManager gameManager;
    public static Board BoardInstance { get; private set; }
    public BoardHighlighter BoardHighlighter { get; private set; }

    public Cell[,] CellLayer { get; private set; }
    public GameObject[,] PlayerLayer{ get; private set; }
    public int Height { get; private set; }
    public int Width { get; private set;}
    
    [SerializeField] private GameObject cellPrefab;
    
    public GameObject CurrentPlayer => gameManager.GetCurrentPlayerGameObject();

    private void Awake()
    {
        if (Board.BoardInstance == null)
        {
            BoardInstance = this;
        }
        else
        {
            Destroy(BoardInstance);
        }

        gameManager = FindObjectOfType<GameManager>();
        BoardHighlighter = GetComponent<BoardHighlighter>();
    }

    public void MakeBoard(BoardData boardData)
    {
        Height = boardData.height;
        Width = boardData.width;

        CellLayer = new Cell[Width, Height];
        PlayerLayer = new GameObject[Width, Height];

        bool isWhite = true;

        for (int z = 0; z < Height; z++)
        {
            for (int x = 0; x < Width; x++)
            {
                Cell cell = Instantiate(cellPrefab, new Vector3(x, 0, -z), Quaternion.identity, gameObject.transform).GetComponent<Cell>();
                cell.cellPosition = new Vector2Int(x, z);
                CellLayer[x, z] = cell;

                cell.defaultColor = isWhite ? Color.white : Color.black;
                cell.UpdateState(CellState.DEFAULT);

                isWhite = !isWhite;
            }

            if (Width % 2 == 0)
            {
                isWhite = !isWhite;
            }
        }
    }

    public Vector2Int GetPlayerPosition(GameObject player)
    {
        for (int z = 0; z < Height; z++)
        {
            for (int x = 0; x < Width; x++)
            {
                if (PlayerLayer[x, z] == player)
                {
                    return new Vector2Int(x, z);
                }
            }
        }
        return new Vector2Int(-1, -1);
    }

    public void ChangePlayerPos(GameObject player, Vector2Int cellPos)
    {
        Vector2Int playerPos = GetPlayerPosition(player);
        PlayerLayer[playerPos.x, playerPos.y] = null;
        
        PlayerLayer[cellPos.x, cellPos.y] = player;
        /*BoardHighlighter.DehighlightCells();*/
        //PlacePlayer(player, cellPos);
    }

    public void PlacePlayer(GameObject player, Vector2Int pos)
    {
        PlayerLayer[pos.x, pos.y] = player;
        player.transform.position = CellLayer[pos.x, pos.y].transform.position;
        player.transform.position += Vector3.up * 1;
    }
/*
    public Cell this[int x, int z]
    {
        get
        {
            return CellLayer[x, z];
        }
    }*/
    
    public List<Cell> GetCells
    {
        get
        {
            List<Cell> _return = new List<Cell>();

            for (int z = 0; z < Height; z++)
            {
                for (int x = 0; x < Width; x++)
                {
                    _return.Add(CellLayer[x, z]);
                }
            }

            return _return;
        }
    }
    
}

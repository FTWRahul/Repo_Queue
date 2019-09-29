using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    [SerializeField] private GameObject cellPrefab;

    public static Board boardInstance { get; private set; }

    public Cell[,] CellLayer { get; private set; }
    private GameObject[,] _playerLayer;
    private int _height;
    private int _width;

    private void Start()
    {
        if (Board.boardInstance == null)
        {
            boardInstance = this;
        }
        else
        {
            Destroy(boardInstance);
        }
    }

    public void MakeBoard(BoardData boardData)
    {

        _height = boardData.height;
        _width = boardData.width;

        CellLayer = new Cell[_width, _height];
        _playerLayer = new GameObject[_width, _height];

        bool isWhite = true;

        for (int z = 0; z < _height; z++)
        {
            for (int x = 0; x < _width; x++)
            {
                Cell cell = Instantiate(cellPrefab, new Vector3(x, 0, -z), Quaternion.identity, gameObject.transform).GetComponent<Cell>();
                cell.cellPosition = new Vector2Int(x, z);
                CellLayer[x, z] = cell;

                cell.defaultColor = isWhite ? Color.white : Color.black;
                cell.Dehighlight();

                isWhite = !isWhite;
            }

            if (_width % 2 == 0)
            {
                isWhite = !isWhite;
            }
        }
    }

    public Vector2Int GetPlayerPosition(GameObject player)
    {
        for (int z = 0; z < _height; z++)
        {
            for (int x = 0; x < _width; x++)
            {
                if (_playerLayer[x, z] == player)
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
        _playerLayer[playerPos.x, playerPos.y] = null;
        
        _playerLayer[cellPos.x, cellPos.y] = player;
        
        //PlacePlayer(player, cellPos);
    }


    public void HighlightMovementCells(GameObject player)
    {
        Vector2Int playerPos = GetPlayerPosition(player);
        
        foreach (Pattern pattern in player.GetComponent<Player>().movementPatterns)
        {
            foreach (Vector2Int pos in pattern.positions)
            {
                Vector2Int resultingPos = playerPos + pos;
                
                if (resultingPos.x < 0 || resultingPos.x > _width - 1 || resultingPos.y < 0 || resultingPos.y > _height - 1) // outside of the board
                {
                    break;
                }

                if (_playerLayer[resultingPos.x, resultingPos.y] != null) // player is on cell
                {
                    break;
                }
                
                CellLayer[resultingPos.x, resultingPos.y].Highlight();
            }
        }
    }

    public void PlacePlayer(GameObject player, Vector2Int pos)
    {
        _playerLayer[pos.x, pos.y] = player;
        player.transform.position = CellLayer[pos.x, pos.y].transform.position;
        player.transform.position += Vector3.up * 1;
    }

    public Cell this[int x, int z]
    {
        get
        {
            return CellLayer[x, z];
        }
    }


    private List<Cell> Cells
    {
        get
        {
            List<Cell> _return = new List<Cell>();

            for (int z = 0; z < _height; z++)
            {
                for (int x = 0; x < _width; x++)
                {
                    _return.Add(CellLayer[x, z]);
                }
            }

            return _return;
        }
    }
    
    public void DehighlightCells()
    {
        foreach (Cell cell in Cells)
        {
            cell.Dehighlight();
        }
    }
}

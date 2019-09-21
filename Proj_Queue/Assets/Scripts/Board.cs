using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    [SerializeField] private GameObject cellPrefab;

    private Cell[,] _cellLayer;
    private GameObject[,] _playerLayer;
    private int _height;
    private int _width;

    public void MakeBoard(BoardData boardData)
    {

        _height = boardData.height;
        _width = boardData.width;

        _cellLayer = new Cell[_width, _height];
        _playerLayer = new GameObject[_width, _height];

        bool isWhite = true;

        for (int z = 0; z < _height; z++)
        {
            for (int x = 0; x < _width; x++)
            {
                Cell cell = Instantiate(cellPrefab, new Vector3(x, 0, -z), Quaternion.identity, gameObject.transform).GetComponent<Cell>();
                cell.CellPosition = new Vector2Int(x, z);
                _cellLayer[x, z] = cell;

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

    public void MovePlayer(GameObject player, Vector2Int cellPos)
    {
        Vector2Int playerPos = GetPlayerPosition(player);
        _playerLayer[playerPos.x, playerPos.y] = null;

        PlacePlayer(player, cellPos);
    }


    public void HighlightMovementCells(GameObject player)
    {
        Vector2Int playerPos = GetPlayerPosition(player);

        List<Vector2Int> movementPattern = new List<Vector2Int>();

        foreach (Vector2Int pos in player.GetComponent<Player>().movementPattern)
        {
            movementPattern.Add(playerPos + pos);
        }

        movementPattern.RemoveAll(x => x.x < 0 || x.x > _width - 1 || x.y < 0 || x.y > _height - 1);

        for (int z = 0; z < _height; z++)
        {
            for (int x = 0; x < _width; x++)
            {
                if (_playerLayer[x, z] != null)
                {
                    movementPattern.Remove(new Vector2Int(x, z));
                }
            }
        }

        foreach (Vector2Int move in movementPattern)
        {
            _cellLayer[move.x, move.y].Highlight();
        }
    }

    public void PlacePlayer(GameObject player, Vector2Int pos)
    {
        _playerLayer[pos.x, pos.y] = player;
        player.transform.position = _cellLayer[pos.x, pos.y].transform.position;
        player.transform.position += Vector3.up * 1;
    }

    public Cell this[int x, int z]
    {
        get
        {
            return _cellLayer[x, z];
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
                    _return.Add(_cellLayer[x, z]);
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

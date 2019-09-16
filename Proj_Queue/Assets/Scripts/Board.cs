using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    [SerializeField]
    GameObject cellPrefab;

    Cell[,] cellLayer;
    GameObject[,] playerLayer;

    int height;
    int width;

    public void MakeBoard(BoardData boardData)
    {

        height = boardData.height;
        width = boardData.width;

        cellLayer = new Cell[height, width];
        playerLayer = new GameObject[height, width];

        bool isWhite = true;

        for (int z = 0; z < height; z++)
        {
            for (int x = 0; x < width; x++)
            {
                Cell cell = Instantiate(cellPrefab, new Vector3(x, 0, -z), Quaternion.identity, gameObject.transform).GetComponent<Cell>();
                cell.CellPosition = new Vector2Int(x, z);
                cellLayer[x, z] = cell;

                cell.defaultColor = isWhite ? Color.white : Color.black;
                cell.Dehighlight();

                isWhite = !isWhite;
            }

            if (width % 2 == 0)
            {
                isWhite = !isWhite;
            }
        }
    }

    public void MovePlayer(GameObject player, Vector2Int cellPos)
    {
        for (int z = 0; z < height; z++)
        {
            for (int x = 0; x < width; x++)
            {
                if (playerLayer[x, z] == player)
                {
                    playerLayer[x, z] = null;
                }
            }
        }

        PlacePlayer(player, cellPos);
    }

    public void HighlightMovementCells(GameObject player)
    {
        Vector2Int playerPos = new Vector2Int();

        for (int z = 0; z < height; z++)
        {
            for (int x = 0; x < width; x++)
            {
                if (playerLayer[x, z] == player)
                {
                    playerPos = new Vector2Int(x, z);
                }
            }
        }

        List<Vector2Int> movementPattern = new List<Vector2Int>();

        foreach (Vector2Int pos in player.GetComponent<Player>().movementPattern)
        {
            movementPattern.Add(playerPos + pos);
        }

        movementPattern.RemoveAll(x => x.x < 0 || x.x > width - 1 || x.y < 0 || x.y > height - 1);

        for (int z = 0; z < height; z++)
        {
            for (int x = 0; x < width; x++)
            {
                if (playerLayer[x, z] != null)
                {
                    movementPattern.Remove(new Vector2Int(x, z));
                }
            }
        }

        foreach (Vector2Int move in movementPattern)
        {
            cellLayer[move.x, move.y].Highlight();
        }
    }

    public void PlacePlayer(GameObject player, Vector2Int pos)
    {
        playerLayer[pos.x, pos.y] = player;
        player.transform.position = cellLayer[pos.x, pos.y].transform.position;
        player.transform.position += Vector3.up * 1;
    }

    public Cell this[int x, int z]
    {
        get
        {
            return cellLayer[x, z];
        }
    }


    public List<Cell> Cells
    {
        get
        {
            List<Cell> _return = new List<Cell>();

            for (int z = 0; z < height; z++)
            {
                for (int x = 0; x < width; x++)
                {
                    _return.Add(cellLayer[x, z]);
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

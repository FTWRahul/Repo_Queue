using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Board
{
    Cell [,] board;
    int height;
    int width;

    public Cell this[int i, int j]
    {
        get
        {
            return board[i, j];
        }
    }

    public List<Cell> Cells
    {
        get
        {
            List<Cell> _return = new List<Cell>();

            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    _return.Add(board[i, j]);
                }
            }

            return _return;
        }
    }

    public Board(BoardData boardData)
    {
        height = boardData.height;
        width = boardData.width;
        board = new Cell[boardData.height, boardData.width];

        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                board[i, j] = new Cell(new Vector2Int(i, j));
            }
        }
    }

    public void PlaceObject(GameObject obj, Vector2Int pos)
    {
        board[pos.x, pos.y].AddObject(obj);
    }

    public void PlaceObject(GameObject obj, Cell cell)
    {
        Cells.Find(x => x == cell).AddObject(obj);
    }

}

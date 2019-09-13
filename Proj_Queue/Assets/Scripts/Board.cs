using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Board
{
    List<List<Cell>> board;

    public Board(BoardData boardData)
    {
        board = new List<List<Cell>>(); 

        for (int i = 0; i < boardData.height; i++)
        {
            for (int j = 0; j < boardData.width; j++)
            {
                board[i][j] = new Cell(new Vector2(i, j));
            }
        }
    }

    public Cell Cell(int i, int j)
    {
        //i - row
        //j - column

        return board[i][j];
    }

    public List<Cell> Cells
    {
        get
        {
            List<Cell> _return = new List<Cell>();

            for (int i = 0; i < board.Count; i++)
            {
                for (int j = 0; j < board[i].Count; j++)
                {
                    _return.Add(board[i][j]);
                }
            }

            return _return;
        }
    }
}

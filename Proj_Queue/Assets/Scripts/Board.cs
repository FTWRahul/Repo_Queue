using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Board
{
    public List<List<Cell>> board;

    public Board(BoardData boardData)
    {
        board = new List<List<Cell>>(); 

        for (int i = 0; i < boardData.height; i++)
        {
            for (int j = 0; j < boardData.width; j++)
            {
                board[i][j] = new Cell();
            }
        }
    }
}

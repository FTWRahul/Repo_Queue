using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    [SerializeField]
    GameObject tile;

    Cell[,] board;
    int height;
    int width;

    public void MakeBoard(BoardData boardData)
    {

        height = boardData.height;
        width = boardData.width;
        board = new Cell[boardData.height, boardData.width];

        for (int z = 0; z < height; z++)
        {
            for (int x = 0; x < width; x++)
            {
                Cell cell = Instantiate(tile, new Vector3(x, 0, -z), Quaternion.identity, gameObject.transform).GetComponent<Cell>();
                cell.CellPosition = new Vector2Int(x, z);
                board[x, z] = cell;
            }
        }
    }

    /// <summary>
    /// Gets the cell at given J and I coordinate
    /// </summary>
    /// <param name="x"></param>
    /// <param name="z"></param>
    /// <returns></returns>
    public Cell this[int x, int z]
    {
        get
        {
            return board[x, z];
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
                    _return.Add(board[x, z]);
                }
            }

            return _return;
        }
    }

}

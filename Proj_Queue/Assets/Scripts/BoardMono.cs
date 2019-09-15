using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardMono : MonoBehaviour
{
    //public BoardData boardData;


    [SerializeField]
    GameObject tile;

    CellMono[,] board;
    int height;
    int width;

    // Start is called before the first frame update
    void Awake()
    {
        //Initilize(boardData);
    }

    [ContextMenu("Make Board")]
    public void MakeBoard(BoardData boardData)
    {

        height = boardData.height;
        width = boardData.width;
        board = new CellMono[boardData.height, boardData.width];

        for (int z = 0; z < height; z++)
        {
            for (int x = 0; x < width; x++)
            {
                CellMono cell = Instantiate(tile, new Vector3(x,0,z), Quaternion.identity).GetComponent<CellMono>();
                cell.CellPositon = new Vector2Int(x, z);
                board[x, z] = cell;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Gets the cell at given J and I coordinate
    /// </summary>
    /// <param name="x"></param>
    /// <param name="z"></param>
    /// <returns></returns>
    public CellMono this[int x, int z]
    {
        //i - row
        //j - column
        get
        {
            return board[x, z];
        }
    }


    public List<CellMono> Cells
    {
        get
        {
            List<CellMono> _return = new List<CellMono>();

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

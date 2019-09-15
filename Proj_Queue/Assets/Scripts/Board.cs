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

                var block = new MaterialPropertyBlock();
                block.SetColor("_BaseColor", isWhite ? Color.white : Color.black);
                cell.GetComponent<Renderer>().SetPropertyBlock(block);

                isWhite = !isWhite;
            }

            if (width % 2 == 0)
            {
                isWhite = !isWhite;
            }
        }
    }

    public void PlacePlayer(GameObject player, Vector2Int pos)
    {
        playerLayer[pos.x, pos.y] = player;
        player.transform.position = cellLayer[pos.x, pos.y].transform.position;
        player.transform.position += Vector3.up * 1;
    }

    //public void MakePlayer(GameObject playerPrefab, PlayerData playerData)
    //{
    //    cellLayer = new Cell[boardData.height, boardData.width];

    //    for (int z = 0; z < height; z++)
    //    {
    //        for (int x = 0; x < width; x++)
    //        {
    //            Cell cell = Instantiate(cellPrefab, new Vector3(x, 0, -z), Quaternion.identity, gameObject.transform).GetComponent<Cell>();
    //            cell.CellPosition = new Vector2Int(x, z);
    //            cellLayer[x, z] = cell;
    //        }
    //    }
    //}

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

}

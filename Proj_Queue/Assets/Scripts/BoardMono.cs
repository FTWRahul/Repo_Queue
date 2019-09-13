using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardMono : MonoBehaviour
{
    public BoardData boardData;

    Board board;

    [SerializeField]
    GameObject tile;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    [ContextMenu("GenerateBoard")]
    public void GenerateBoard()
    {
        board = new Board(boardData);
        foreach (Cell cell in board.Cells)
        {
           GameObject newCell = Instantiate(tile, new Vector3(cell.CellPositon.x, 0, cell.CellPositon.y), Quaternion.identity);
            newCell.GetComponent<CellMono>().cellInfo = cell;
           /// Debug.Log(cell.CellPositon.x);
        }
    }
}

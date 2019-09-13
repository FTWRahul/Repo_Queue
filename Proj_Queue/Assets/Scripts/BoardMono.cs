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
        for (int i = 0; i < board._board.height; i++)
        {
            for (int j = 0; j < board._board.width; j++)
            {
                board._board[i][j] = new Cell();
            }
        }
    }
}

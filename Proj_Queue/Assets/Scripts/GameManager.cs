using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Board board;
    public BoardData boardData;

    void Start()
    {
        board.MakeBoard(boardData);
    }
}

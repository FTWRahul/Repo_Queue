using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardHighlighter : MonoBehaviour
{
    private Board _board;

    private void Start()
    {
        _board = GetComponent<Board>();
    }

    public void HighlightCells(PatternData pattern)
    {
        foreach (Vector2Int pos in pattern.positions)
        {
             //Vector2Int resultingPos = origin + pos;
            _board.CellLayer[pos.x, pos.y].Highlight();
        }
    }
    
    public void HighlightMovementCells(GameObject player)
    {
        Vector2Int playerPos = _board.GetPlayerPosition(player);
        
        foreach (PatternData pattern in player.GetComponent<Player>().movementPatterns)
        {
            foreach (Vector2Int pos in pattern.positions)
            {
                Vector2Int resultingPos = playerPos + pos;
                
                if (resultingPos.x < 0 || resultingPos.x > _board.Width - 1 || resultingPos.y < 0 || resultingPos.y > _board.Height - 1) // outside of the board
                {
                    break;
                }

                if (_board.PlayerLayer[resultingPos.x, resultingPos.y] != null) // player is on cell
                {
                    break;
                }
                
                _board.CellLayer[resultingPos.x, resultingPos.y].Highlight();
            }
        }
    }
    
    public void DehighlightCells()
    {
        foreach (Cell cell in _board.Cells)
        {
            cell.Dehighlight();
        }
    }
}

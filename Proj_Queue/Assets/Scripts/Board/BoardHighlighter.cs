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
            _board.CellLayer[pos.x, pos.y].UpdateState(CellState.HIGHLIGHTED);
        }
    }

    public void DehighlightCells()
    {
        foreach (Cell cell in _board.GetCells)
        {
            cell.UpdateState(CellState.DEFAULT);
        }
    }
}

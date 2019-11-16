using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewAction", menuName = "Action", order = 1)]
public class ActionData : ScriptableObject
{
    public string actionName;

    public List<PatternData> patterns;
    public BehaviourData behaviour;

    public Vector2Int targetCell;

    private PatternData selectedPattern;

    public PatternData SelectedPattern
    {
        get => selectedPattern;
        set => selectedPattern = value;
    }

    /// <summary>
    /// Highlights all the possible moves on the board based on the behaviour.
    /// </summary>
    public void DisplayPossiblePattern(Vector2Int origin)
    {
        targetCell = origin;
        List<PatternData> patternToSend = behaviour.InterpretPattern(patterns, origin);

        for (int i = 0; i < patternToSend.Count; i++)
        {
            Board.BoardInstance.BoardHighlighter.HighlightCells(patternToSend[i]);
        }
    }

    public void DisplaySelectedPattern(Vector2Int inPos)
    {
        Vector2Int resetPos = inPos - targetCell;
        PatternData tempPatternData = null;
        foreach (var pat in patterns)
        {
            foreach (var pos in pat.positions)
            {
                if (resetPos == pos)
                {
                    tempPatternData = pat;
                    break;
                }
            }

            if (tempPatternData != null)
            {
                break;
            }
        }

        for (int i = 0; i < tempPatternData.positions.Count; i++)
        {
            tempPatternData.positions[i] += targetCell;
        }
        Board.BoardInstance.BoardHighlighter.HighlightSelectedCells(tempPatternData);
    }
    
    public void SelectPattern(Vector2Int inPos)
    {
        Vector2Int resetPos = inPos - targetCell;
        PatternData tempPatternData = null;
        foreach (var pat in patterns)
        {
            foreach (var pos in pat.positions)
            {
                if (resetPos == pos)
                {
                    tempPatternData = pat;
                    break;
                }
            }

            if (tempPatternData != null)
            {
                SelectedPattern = tempPatternData;
                break;
            }
        }
    }

    public void ExecuteAction()
    {
        behaviour.Execute(selectedPattern, targetCell);
    }
}
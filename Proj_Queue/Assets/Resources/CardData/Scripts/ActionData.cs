using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewAction", menuName = "Action", order = 1)]
public class ActionData : ScriptableObject
{
    public string actionName;

    public List<PatternData> patterns;
    public BehaviourData behaviour;
    
    /// <summary>
    /// Highlights all the possible moves on the board based on the behaviour.
    /// </summary>
    public void DisplayPossiblePattern(Vector2Int origin)
    {
        List<PatternData> patternToSend = behaviour.InterpretPattern(patterns, origin);

        foreach (PatternData pat in patternToSend)
        {
            Board.boardInstance.BoardHighlighter.HighlightCells(pat);
        }
    }
}
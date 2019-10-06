using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewAction", menuName = "Action", order = 1)]
public class ActionData : ScriptableObject
{
    public string actionName;

    public List<PatternData> patterns;
    public BehaviourData behaviour;

    public Vector2Int originCell;
    
    /// <summary>
    /// Highlights all the possible moves on the board based on the behaviour.
    /// </summary>
    public void DisplayPossiblePattern(Vector2Int origin)
    {
        List<PatternData> patternToSend = behaviour.InterpretPattern(patterns, origin);

        for (int i = 0; i < patternToSend.Count; i++)
        {
            //Debug.Log( "Display Pattern is " + patternToSend[i]);
            Board.boardInstance.BoardHighlighter.HighlightCells(patternToSend[i]);
        }
    }
}
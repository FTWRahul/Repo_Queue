using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public struct Pattern
{
    public List<Vector2Int> positions;

    public Pattern(PatternData patternData)
    {
        positions = patternData.positions;
    }
}

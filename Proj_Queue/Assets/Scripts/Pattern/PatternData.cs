using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PatternData", menuName = "PatternData", order = 1)]
public class PatternData : ScriptableObject
{
    public List<Vector2Int> positions;
}

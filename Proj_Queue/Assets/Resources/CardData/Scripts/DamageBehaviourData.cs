using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageBehaviourData : BehaviourData
{
    public int damage;

    public override void Execute()
    {
        Debug.Log("Damage "+ damage );
    }

    public override List<PatternData> InterpretPattern(List<PatternData> patterns, Vector2Int origin)
    {
        patterns = base.InterpretPattern(patterns, origin);
        List<PatternData> returnList = new List<PatternData>();
                                              
        foreach (PatternData pat in patterns)
        {
            PatternData tempPat = pat;
            foreach (Vector2Int pos in pat.positions)
            {
                Vector2Int resultingPos = origin + pos;
                                              
               // Conditions for modifying the pattern
               
               tempPat.positions.Add(pos);
            }
            returnList.Add(tempPat);
        }
        return returnList;
    }
}

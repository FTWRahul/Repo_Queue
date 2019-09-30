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

    public override List<Pattern> InterpretPattern(List<Pattern> patterns, Vector2Int origin)
    {
        patterns = base.InterpretPattern(patterns, origin);
        List<Pattern> returnList = new List<Pattern>();
                                              
        foreach (Pattern pat in patterns)
        {
            Pattern tempPat = pat;
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

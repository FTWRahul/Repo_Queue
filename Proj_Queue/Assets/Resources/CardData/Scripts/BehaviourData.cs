using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BaseBehaviour")]
public class BehaviourData : ScriptableObject
{
    protected List<PatternData> selectedPattern;
        
        /// <summary>
        /// Takes a pattern and its origin point and removes all the patterns that fall outside board.
        /// </summary>
        /// <param name="patterns"></param>
        /// <param name="origin"></param>
        /// <returns></returns>
        public virtual List<PatternData> InterpretPattern(List<PatternData> patterns, Vector2Int origin)
        {
            List<PatternData> returnList = new List<PatternData>();
            foreach (PatternData pat in patterns)
            {
                PatternData tempPat = CreateInstance<PatternData>();
                List<Vector2Int> returnPat = new List<Vector2Int>();
                foreach (Vector2Int pos in pat.positions)
                {
                    Vector2Int resultingPos = origin + pos;
                
                    if (resultingPos.x < 0 || resultingPos.x > Board.BoardInstance.Width - 1 || resultingPos.y < 0 || resultingPos.y > Board.BoardInstance.Height - 1) // outside of the board
                    {
                        break;
                    }
                    returnPat.Add(resultingPos);
                }

                tempPat.positions = returnPat;
                returnList.Add(tempPat);
            }

            return returnList;
        }

        public virtual void Execute()
        {
            
        }

        public virtual void Execute(PatternData pattern, Vector2Int origin)
        {
            List<PatternData> tempList = new List<PatternData> {pattern};
            List<PatternData> useList = InterpretPattern(tempList, origin);

            foreach (PatternData pat in useList)
            {
                foreach (Vector2Int pos in pat.positions)
                {
                    Debug.Log("Tile being affected is " + pos);
                }
            }
        }
}

   
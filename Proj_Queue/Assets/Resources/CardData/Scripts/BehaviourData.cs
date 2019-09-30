using System.Collections.Generic;
using UnityEngine;


public class BehaviourData : ScriptableObject
{
        public virtual void Execute()
        {
               Debug.Log("Here"); 
        }
        
        /// <summary>
        /// Takes a pattern and its origin point and removes all the patterns that fall outside board.
        /// 
        /// </summary>
        /// <param name="patterns"></param>
        /// <param name="origin"></param>
        /// <returns></returns>
        public virtual List<PatternData> InterpretPattern(List<PatternData> patterns, Vector2Int origin)
        {
            List<PatternData> returnList = new List<PatternData>();
                
            foreach (PatternData pat in patterns)
            {
                PatternData tempPat = pat;
                foreach (Vector2Int pos in pat.positions)
                {
                    Vector2Int resultingPos = origin + pos;
                
                    if (resultingPos.x < 0 || resultingPos.x > Board.boardInstance.Width - 1 || resultingPos.y < 0 || resultingPos.y > Board.boardInstance.Height - 1) // outside of the board
                    {
                        break;
                    }
                    tempPat.positions.Add(pos);
                    //Board.boardInstance.CellLayer[resultingPos.x, resultingPos.y].Highlight();
                }
                returnList.Add(tempPat);
            }

            return returnList;


        }
}

   
using System.Collections.Generic;
using UnityEngine;


public class BehaviourData : ScriptableObject
{
        public virtual void Execute()
        {
               Debug.Log("Here"); 
        }
        
        //Takes pattern and calls the function to highlight the moves
        public virtual List<Pattern> InterpretPattern(List<Pattern> patterns, Vector2Int origin)
        {
            List<Pattern> returnList = new List<Pattern>();
                
            foreach (Pattern pat in patterns)
            {
                Pattern tempPat = pat;
                foreach (Vector2Int pos in pat.positions)
                {
                    Vector2Int resultingPos = origin + pos;
                
                    if (resultingPos.x < 0 || resultingPos.x > Board.boardInstance.Width - 1 || resultingPos.y < 0 || resultingPos.y > Board.boardInstance.Height - 1) // outside of the board
                    {
                        break;
                    }
                    tempPat.positions.Add(pos);
                    

                    /*if (Board.boardInstance.PlayerLayer[resultingPos.x, resultingPos.y] != null) // player is on cell
                    {
                        break;
                    }*/
                    
                    
                    //Board.boardInstance.CellLayer[resultingPos.x, resultingPos.y].Highlight();
                }
                returnList.Add(tempPat);
            }

            return returnList;


        }
}

   
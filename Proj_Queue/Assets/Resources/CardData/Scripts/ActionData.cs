using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewAction", menuName = "Action", order = 1)]
public class ActionData : ScriptableObject
{
    public string actionName;
    
    public PatternData patterns;
    public BehaviourData behaviours;
    

}
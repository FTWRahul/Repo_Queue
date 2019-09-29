using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "NewAction", menuName = "Action", order = 1)]
public class ActionData : ScriptableObject
{
    public string actionName;
    
    public List<PatternData> patterns;
    [FormerlySerializedAs("behaviours")] public BehaviourData behaviour;

}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewAction", menuName = "Actions", order = 1)]
public class ActionData : ScriptableObject
{
    public List<PatternData> patterns;
   // public DamageBehaviour behaviour;
   public virtual void Execute(GameObject target)
    {
        
    }
}


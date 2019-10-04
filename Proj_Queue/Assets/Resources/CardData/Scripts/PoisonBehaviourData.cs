using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Poison", menuName = "Behaviour poison", order = 1)]
public class PoisonBehaviourData : BehaviourData
{
    public int damage;
    public int turns;

    public override void Execute()
    {
        Debug.Log("Damage " + damage + " turns "+ turns);
    }
}

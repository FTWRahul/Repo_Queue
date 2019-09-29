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
}

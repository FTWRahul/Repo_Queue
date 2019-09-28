using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "Behiviour", menuName = "Behaviour", order =  1)]
public class DamageBehaviour : ScriptableObject
{
    public int damage;
    
    public void Execute(GameObject target)
    {
        target.GetComponent<Player>().TakeDamage(damage);
    }
}

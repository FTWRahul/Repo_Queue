using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IChangeBehaviour
{
    new void Execute(GameObject target);
}

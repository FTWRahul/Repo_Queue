using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Cell
{
    [SerializeField]
    List<GameObject> objectsOnTheCell = new List<GameObject>();

    public List<GameObject> ObjectsOnTheCell { get { return objectsOnTheCell; } }
}

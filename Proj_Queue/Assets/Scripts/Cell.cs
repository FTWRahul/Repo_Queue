using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Cell
{
    [SerializeField]
    List<GameObject> objectsOnTheCell = new List<GameObject>();
    public List<GameObject> ObjectsOnTheCell { get { return objectsOnTheCell; } }

    [SerializeField]
    Vector2 cellPosition;
    public Vector2 CellPositon { get { return cellPosition; } set { cellPosition = value; } }

    public Cell(Vector2 _cellPosition)
    {
        CellPositon = _cellPosition;
    }
}

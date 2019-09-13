using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Cell
{
    [SerializeField]
    List<GameObject> objectsOnTheCell = new List<GameObject>();
    public List<GameObject> ObjectsOnTheCell { get { return objectsOnTheCell; } set { objectsOnTheCell = value; } }

    [SerializeField]
    Vector2 cellPosition;
    public Vector2 CellPositon { get { return cellPosition; } set { cellPosition = value; } }

    public Cell(Vector2 _cellPosition)
    {
        this.CellPositon = _cellPosition;
    }

    void AddObjectOnTheCell(GameObject _object)
    {
        ObjectsOnTheCell.Add(_object);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    [SerializeField]
    Vector2Int gridPosition;
    public Vector2Int CellPositon { get { return gridPosition; } set { gridPosition = value; } }
}


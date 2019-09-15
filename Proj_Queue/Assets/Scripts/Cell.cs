using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    [SerializeField]
    Vector2Int cellPosition;
    public Vector2Int CellPosition { get { return cellPosition; } set { cellPosition = value; } }
}


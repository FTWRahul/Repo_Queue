using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellMono : MonoBehaviour
{
    [SerializeField]
    Vector2Int cellPosition;
    public Vector2Int CellPositon { get { return cellPosition; } set { cellPosition = value; } }
}

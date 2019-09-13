using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellMono : MonoBehaviour
{
    [SerializeField]
    Cell cellInfo;

    private void Awake()
    {
        Cell cellInfo = new Cell();
    }
}

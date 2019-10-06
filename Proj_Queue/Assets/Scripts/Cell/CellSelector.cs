using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellSelector : MonoBehaviour
{
    [SerializeField] private GameManager gm;
    [SerializeField] private LayerMask cellLayerMask;
    private Camera _camera;

    public delegate void CellSelectorDelegate(Vector2Int cellPos);
    public event CellSelectorDelegate OnCellSelectedEvent = delegate { };

    private void Awake()
    {
        gm = FindObjectOfType<GameManager>();
        _camera = Camera.main;
    }

    private void Update()
    {
        if (!Input.GetMouseButtonDown(0)) return;
        
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

        if (!Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, cellLayerMask)) return;
        
        if (!hit.collider.GetComponent<Cell>().highlighted) return;
        
        Vector2Int cellPos = hit.collider.gameObject.GetComponent<Cell>().cellPosition;

        OnCellSelectedEvent(cellPos);
/*        gm.CurrentPlayer.GetComponent<Player>().Move(cellPos);*/
    }
}

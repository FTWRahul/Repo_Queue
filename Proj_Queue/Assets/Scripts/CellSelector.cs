using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellSelector : MonoBehaviour
{
    [SerializeField] private GameManager gm;
    [SerializeField] private LayerMask cellLayerMask;
    private Camera _camera;

    private void Awake()
    {
        gm = FindObjectOfType<GameManager>();
        _camera = Camera.main;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, cellLayerMask))
            {
                if (hit.collider.GetComponent<Cell>().highlighted)
                {
                    Vector2Int cellPos = hit.collider.gameObject.GetComponent<Cell>().CellPosition;
                    gm.MakeMove(cellPos);
                }
            }
        }
    }
}

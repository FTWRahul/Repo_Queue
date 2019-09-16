using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellSelector : MonoBehaviour
{
    public GameManager gm;

    [SerializeField]
    LayerMask cellLayerMask;

    Camera cam;

    private void Awake()
    {
        cam = Camera.main;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);

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

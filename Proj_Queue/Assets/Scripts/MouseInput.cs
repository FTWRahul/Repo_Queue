using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseInput : MonoBehaviour
{
    [SerializeField]
    int cellLayerMask;

    [SerializeField]
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
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, cellLayerMask))
            {
                Vector2Int cellPos = hit.collider.gameObject.GetComponent<Cell>().CellPosition;
                Debug.Log(cellPos);
            }
        }
    }
}

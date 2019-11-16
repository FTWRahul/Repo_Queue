using UnityEngine;

public class CellSelector : MonoBehaviour
{
    [SerializeField] private LayerMask cellLayerMask;
    private Camera _camera;

    public delegate void CellSelectorDelegate(Vector2Int cellPos);
    public event CellSelectorDelegate CellHitEvent = delegate { };

    private Cell currentCellSelected;
    private Cell previousCellSelected;

    private void Awake()
    {
        _camera = Camera.main;
    }

    private void Update()
    {
/*
        previousCellSelected = currentCellSelected;
   
        if (previousCellSelected)
        {
            previousCellSelected.UpdateState(CellState.HIGHLIGHTED);
        }
        */

        RaycastHit hit;
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
        
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, cellLayerMask))
        {
            Cell cell = hit.collider.GetComponent<Cell>();

            if (cell.state != CellState.DEFAULT)
            {
                currentCellSelected = cell;
                Vector2Int cellPos = cell.cellPosition;

                if (currentCellSelected != previousCellSelected)
                {
                    previousCellSelected.UpdateState(CellState.HIGHLIGHTED);
                }
                else
                {
                    cell.UpdateState(CellState.HOVERED);
                }
                
                if (Input.GetMouseButtonDown(0))
                {
                    OnCellHitEvent(cellPos);
                }
            }
        }
    }


    protected virtual void OnCellHitEvent(Vector2Int cellPos)
    {
        CellHitEvent?.Invoke(cellPos);
    }
}

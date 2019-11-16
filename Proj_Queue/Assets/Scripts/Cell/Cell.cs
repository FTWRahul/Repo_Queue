using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public enum CellState
{
    DEFAULT,
    HIGHLIGHTED,
    HOVERED
}
public class Cell : MonoBehaviour
{
    public Vector2Int cellPosition;

    public Color defaultColor;
    public Renderer cellRenderer;
    /*public bool highlighted;*/

    public CellState state;
    
    [SerializeField] private Color highlightedColor;
    [SerializeField] private Color hoveredColor;

    public void Awake()
    {
        cellRenderer = GetComponent<Renderer>();
        state = CellState.DEFAULT;
    }

    public void UpdateState(CellState newState)
    {
        state = newState;
        switch (state)
        {
            case CellState.DEFAULT:
                cellRenderer.material.color = defaultColor;
                break;
            case CellState.HIGHLIGHTED:
                cellRenderer.material.color = highlightedColor;
                break;
            case CellState.HOVERED:
                cellRenderer.material.color = hoveredColor;
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    public void Highlight()
    {
        /*highlighted = true;*/
        cellRenderer.material.color = highlightedColor;
    }

    public void Dehighlight()
    {
        /*highlighted = false;*/
        cellRenderer.material.color = defaultColor;
    }
}


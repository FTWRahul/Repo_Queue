using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Cell : MonoBehaviour
{
    public Vector2Int cellPosition;

    public Color defaultColor;
    public Renderer cellRenderer;
    public bool highlighted;
    
    [SerializeField] private Color highlightedColor;

    public void Awake()
    {
        cellRenderer = GetComponent<Renderer>();
    }

    public void Highlight()
    {
        highlighted = true;
        cellRenderer.material.color = highlightedColor;
    }

    public void Dehighlight()
    {
        highlighted = false;
        cellRenderer.material.color = defaultColor;
    }
}


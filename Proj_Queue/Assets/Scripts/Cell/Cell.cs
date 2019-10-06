using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Cell : MonoBehaviour
{
    [FormerlySerializedAs("CellPosition")] public Vector2Int cellPosition;

    public Color defaultColor;
    [SerializeField] private Color highlightedColor;
    public bool highlighted;
    public Renderer rend;
    


    public void Awake()
    {
        rend = GetComponent<Renderer>();
        
    }

    public void Highlight()
    {
        highlighted = true;
        rend.material.color = highlightedColor;
    }

    public void Dehighlight()
    {
        highlighted = false;
        rend.material.color = defaultColor;
    }
}


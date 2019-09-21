using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    [SerializeField] private Vector2Int cellPosition;
    public Vector2Int CellPosition { get => cellPosition; set => cellPosition = value; }

    public Color defaultColor;
    [SerializeField] private Color highlightedColor;
    public bool highlighted;
    
    private static readonly int BaseColor = Shader.PropertyToID("_BaseColor");

    public void Highlight()
    {
        highlighted = true;
        var block = new MaterialPropertyBlock();
        block.SetColor(BaseColor, highlightedColor);
        GetComponent<Renderer>().SetPropertyBlock(block);
    }

    public void Dehighlight()
    {
        highlighted = false;
        var block = new MaterialPropertyBlock();
        block.SetColor(BaseColor, defaultColor);
        GetComponent<Renderer>().SetPropertyBlock(block);
    }
}


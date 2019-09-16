using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    [SerializeField]
    Vector2Int cellPosition;
    public Vector2Int CellPosition { get { return cellPosition; } set { cellPosition = value; } }

    public Color defaultColor;
    public Color highlightedColor;
    public bool highlighted;

    public void Highlight()
    {
        highlighted = true;
        var block = new MaterialPropertyBlock();
        block.SetColor("_BaseColor", highlightedColor);
        GetComponent<Renderer>().SetPropertyBlock(block);
    }

    public void Dehighlight()
    {
        highlighted = false;
        var block = new MaterialPropertyBlock();
        block.SetColor("_BaseColor", defaultColor);
        GetComponent<Renderer>().SetPropertyBlock(block);
    }
}


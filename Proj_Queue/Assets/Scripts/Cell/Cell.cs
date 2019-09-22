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


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering;

public class PropertyHolder : MonoBehaviour, IWantEditor
{
    private int health;
    
    public int Health
    {
        get => health;
        set => health = value;
    }

    public void ReturnAllProperties()
    {
        
    }
}

public interface IWantEditor
{
    void ReturnAllProperties();
}

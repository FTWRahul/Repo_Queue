using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMono : MonoBehaviour
{
    [SerializeField]
    public Player playerInfo;

    private void Start()
    {
        GetComponent<MeshRenderer>().material.color = playerInfo.Color;
    }
}

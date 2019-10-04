using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BoardData", menuName = "BoardData", order = 1)]
public class BoardData : ScriptableObject
{
    public int height;
    public int width;
}

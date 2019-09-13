using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardMono : MonoBehaviour
{
    [SerializeField]
    List<List<int>> coordinates;
    [SerializeField]
    List<int> internalStuff;
    public List<List<int>> Coordinates
    {
        get
        {
            return this.coordinates;
        }
        set
        {
            this.coordinates = value;
        }
    }

    [SerializeField]
    GameObject tile;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    [ContextMenu("GenerateBoard")]
    public void GenerateBoard()
    {
        Coordinates.Add(internalStuff);

        int i = 0;
        foreach (List<int> row in coordinates)
        {
            foreach (var collom in row)
            {
                Instantiate(tile, new Vector3(i, 0, collom), Quaternion.identity);
            }
            i++;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseInputMono : MonoBehaviour
{

    public Camera cam;
    RaycastHit hit;
    [SerializeField]
    LayerMask myLayerMask;

    GameObject target;

    GameObject Board;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {        
        if(Input.GetMouseButtonDown(0))
        {
            SelectTarget();
        }
    }

    void SelectTarget()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray, out hit, myLayerMask))
        {
           //if(hit.collider.GetComponent<IRespondToClick>() != null)
           //{
           //    hit.collider.GetComponent<IRespondToClick>().OnClick();
           //}
        }
    }
}


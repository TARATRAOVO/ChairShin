using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public Vector3 mousePos;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        mousePos = Input.mousePosition;
        // Ray myRay = Camera.main.ScreenPointToRay(new Vector3(mousePos.x, mousePos.y, Camera.main.nearClipPlane));
        Ray myRay = Camera.main.ScreenPointToRay(mousePos);

        RaycastHit hit;

        if (Physics.Raycast(myRay, out hit))
        {
            transform.position = new Vector3(hit.point.x, 0.0f, hit.point.z);
        }

        // print(mousePos);
        // print(Camera.main.ScreenToWorldPoint(mousePos));
        
        // transform.position = mousePos;
    }
}

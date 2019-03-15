using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockInteraction : MonoBehaviour
{
    public Camera arCamera;
    public float minimumDistance  = 100;
    public GameObject go;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Starts the arcamera script");
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("updates the arcamera script");
        if (Input.mousePresent == true)
        {
            // Touches performed on screen
            Ray ray;
            RaycastHit hit;
            //Debug.Log("2");

            if (Input.GetMouseButton(0))
            {
                Debug.Log("Mouse is clicked");
                ray = arCamera.ScreenPointToRay(Input.mousePosition);
                hit = new RaycastHit();
                //Debug.Log("33");
                if (Physics.Raycast(ray, out hit))
                {

                    if (hit.collider.gameObject.tag == "jengaBlock")
                    {
                        Debug.Log("Hits jenga block");
                        go = hit.transform.gameObject;
                        Debug.Log("Touch Detected on : " + go.name);
                        
                    }

                }
            }
        }

        /*RaycastHit hit;
        Touch touch = Input.GetTouch(0);
        Ray pointToBlock = new Ray(arCamera.transform.position, touch.position);

        if(Physics.Raycast(pointToBlock, out hit, minimumDistance))
        {
            if(hit.collider.tag == "block")
            {
                Debug.Log("Hit Something");
            }
        }*/
    }
}

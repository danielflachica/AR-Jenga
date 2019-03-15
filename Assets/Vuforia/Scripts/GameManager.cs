using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public Camera arCamera;
    public GameObject tower;
    public GameObject plane;
    public GameObject planeFinder;
    private bool isPlaced = false;
    private bool isKinematic = true;

    // Start is called before the first frame update
    void Start()
    {
           
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray;
        RaycastHit hit;

        if (planeFinder.activeSelf == false && isPlaced == false)
        {
            tower.active = true;
            Debug.Log("Tower has been placed");

            int piecesCount = tower.transform.GetChildCount();

            if (isKinematic)
            {
                unKinematic(piecesCount);
                isKinematic = false;
            }

            isPlaced = true;
        }



        if (Input.GetMouseButton(0))
        {
            Debug.Log("Mouse is clicked");
            ray = arCamera.ScreenPointToRay(Input.mousePosition);
            hit = new RaycastHit();
            //Debug.Log("33");
            if (Physics.Raycast(ray, out hit, 50))
            {
                if (hit.collider.gameObject.tag == "jengaBlock")
                {
                    Debug.Log("Hits jenga block");
                    tower = hit.transform.gameObject;
                    Debug.Log("Touch Detected on : " + tower.name);

                }

            }
        }


    }

    public void unKinematic(int pieces)
    {
        Debug.Log("Count of Jenga Pieces: "+ pieces);
        GameObject piece;

        for (int i = 0; i < pieces; i++)
        {
            piece = tower.transform.GetChild(i).gameObject;
            Rigidbody rb = piece.GetComponent<Rigidbody>();
            rb.isKinematic = false;
        }
    }

    
}

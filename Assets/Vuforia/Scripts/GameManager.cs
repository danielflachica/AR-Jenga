﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public Camera arCamera;
    public GameObject tower;
    public GameObject block;
    public GameObject plane;
    public GameObject planeFinder;
    public GameObject canvas;
    private bool isPlaced = false;
    private bool isKinematic = true;
    private BlockInteraction interactor;

    // Start is called before the first frame update
    void Start()
    {
        interactor = new BlockInteraction();
    }

    // Update is called once per frame
    void Update()
    {
            

        // if player touches the screen
        if (Input.GetMouseButton(0))
        {
            Ray ray;
            RaycastHit hit;

            Debug.Log("Mouse is clicked");
            ray = arCamera.ScreenPointToRay(Input.mousePosition);
            hit = new RaycastHit();

            //if player taps a block within range
            if (Physics.Raycast(ray, out hit, 50))
            {
                if (hit.collider.gameObject.tag == "jengaBlock")
                {
                    Debug.Log("Hits jenga block");
                    block = hit.transform.gameObject;
                    Debug.Log("Touch Detected on block: " + block.name);

                    interactor.highlightObject(block);

                    enableCanvas();
                }

            }
        }

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

    public void enableCanvas()
    {
        canvas.SetActive(true);
    }

    public void disableCanvas()
    {
        canvas.SetActive(false);
    }
    
}

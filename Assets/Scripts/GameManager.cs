﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public Camera arCamera;
    public GameObject tower;
    public GameObject block;
    public GameObject plane;
    public GameObject planeFinder;
    public GameObject canvas;
    public GameObject blockControlPanel;
    public Button releaseBtn;
    public Text scorePanel;
    private bool isPlaced = false;
    private bool isKinematic = true;
    private bool isTethered = false;
    private BlockInteraction interactor;
    private Vector3 offset;
    private int topStackCount;
    private ScoringScript ss;
    private Vector3 blockOriginPos;
    private bool dropZone;
    public GameObject topOfTower;

    // Time when the movement started.
    private float startTime;

    // Total distance between the markers.
    private float journeyLength;

    // Movement speed in units/sec.
    public float speed = 1.0F;


    // Start is called before the first frame update
    void Start()
    {
        interactor = new BlockInteraction();
        releaseBtn.onClick.AddListener(releaseBlock);
        topStackCount = 0;
        ss = new ScoringScript();
        dropZone = false;
    }

    // Update is called once per frame
    void Update()
    {

        if (isTethered)
        {
            //Debug.Log("Tethered block!");

            // Distance moved = time * speed.
            float distCovered = (Time.time - startTime) * speed;

            // Fraction of journey completed = current distance divided by total distance.
            float fracJourney = distCovered / journeyLength;

            // Set our position as a fraction of the distance between the markers.
            block.transform.position = Vector3.Lerp(block.transform.position, arCamera.transform.position-offset, fracJourney);

            /*Debug.Log("camera position: " + arCamera.transform.localPosition);
            
            Debug.Log("block old position: "+ block.transform.localPosition);
            block.transform.localPosition = arCamera.transform.localPosition + offset;
            Debug.Log("block new position: " + block.transform.localPosition);*/

            
        }

        // if player touches the screen and no block has been selected yet
        if (Input.GetMouseButton(0) && interactor.getStatus() == false)
        {
            Ray ray;
            RaycastHit hit;

            //Debug.Log("Mouse is clicked");
            ray = arCamera.ScreenPointToRay(Input.mousePosition);
            hit = new RaycastHit();

            //if player taps a block within range
            if (Physics.Raycast(ray, out hit, 50))
            {
                if (hit.collider.gameObject.tag == "jengaBlock")
                {

                    //Debug.Log("Hits jenga block");
                    block = hit.transform.gameObject;
                    block.GetComponent<Rigidbody>().useGravity = false;
                    block.GetComponent<Rigidbody>().isKinematic = true;
                    //Debug.Log("Touch Detected on block: " + block.name);
                    offset = arCamera.transform.localPosition - block.transform.localPosition;
                    interactor.setStatus(false);
                    interactor.highlightObject(block);
                    isTethered = true;

                    //to make the block less sensitive to the other blocks
                    block.GetComponent<Rigidbody>().mass = 1;
                    //get origin pos of block
                    blockOriginPos = block.transform.position;

                    enableCanvas();
                    enableBlockControlPanel();

                    // Keep a note of the time the movement started.
                    startTime = Time.time;
                    offset = arCamera.transform.position - block.transform.position;
                    // Calculate the journey length.
                    journeyLength = Vector3.Distance(arCamera.transform.position, block.transform.position);

                    //Debug.Log("Journey Length: " + journeyLength);

                }

            }
        }

        if (planeFinder.activeSelf == false && isPlaced == false)
        {
            tower.SetActive(true);
            Debug.Log("Tower has been placed");
            
            int piecesCount = tower.transform.childCount;

            if (isKinematic)
            {
                unKinematic(piecesCount);
                isKinematic = false;
            }

            isPlaced = true;
        }

        scorePanel.text = "Score: "+ss.getScore();


    }

    public void setDropStatus(bool stat)
    {
        dropZone = stat;
    }

    public void releaseBlock()
    {
        if (dropZone)
            ss.Score();

        block.GetComponent<Rigidbody>().useGravity = true;
        block.GetComponent<Rigidbody>().isKinematic = false;
        isTethered = false;
        disableBlockControlPanel();
        //canvas.SetActive(false);
        interactor.unHighlightObject(block);
        incrementTopStackCount();
        block.GetComponent<Rigidbody>().mass = 3;
        //block = null;

        setDropStatus(false);
    }

    public void incrementTopStackCount()
    {
        if (topStackCount < 3)
            topStackCount++;
        else
            topStackCount = 0;

    }

    public int getTopStackCount()
    {
        return topStackCount;
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

    public void enableBlockControlPanel()
    {

        blockControlPanel.SetActive(true);
     
    }

    public void disableBlockControlPanel()
    {

        blockControlPanel.SetActive(false);
    }

}

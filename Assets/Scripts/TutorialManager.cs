using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{

    public Camera arCamera;
    public GameObject tower;
    public GameObject block;
    public GameObject plane;
    public GameObject planeFinder;
    public GameObject canvas;
    public GameObject blockControlPanel;
    public GameObject tutorialPanel;
    public Button releaseBtn;
    public GameObject topOfTower;

    private bool isPlaced = false;
    private bool isKinematic = true;
    private bool isTethered = false;
    private BlockInteraction interactor;
    private Vector3 offset;
    private int stepNum;
    private bool completedStep = false;
    private Vector3 blockStartPos;

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
        tutorialPanel.SetActive(true);
        stepNum = 1;
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

            //change block position per update
            block.transform.position = Vector3.Lerp(block.transform.position, arCamera.transform.position - offset, fracJourney);

            if (Vector3.Distance(block.transform.position, blockStartPos) > 0.1 && !getStepStatus() && getStep() == 4)
            {
                Debug.Log("Moved enough distance!");
                topOfTower.SetActive(true);
                setCompletedStatus(true);
                incrementStep();

            }
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

                    enableCanvas();
                    enableBlockControlPanel();

                    // Keep a note of the time the movement started.
                    startTime = Time.time;
                    offset = arCamera.transform.position - block.transform.position;
                    blockStartPos = block.transform.position;
                    // Calculate the journey length.
                    journeyLength = Vector3.Distance(arCamera.transform.position, block.transform.position);

                    //Debug.Log("Journey Length: " + journeyLength);

                    if(getStep() == 3)
                    {
                        setCompletedStatus(true);
                        incrementStep();
                    }
                    
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

            setCompletedStatus(true);
            incrementStep();
        }


    }

    public void releaseBlock()
    {
        block.GetComponent<Rigidbody>().useGravity = true;
        block.GetComponent<Rigidbody>().isKinematic = false;
        isTethered = false;
        disableBlockControlPanel();
        interactor.unHighlightObject(block);
        block = null;
        topOfTower.SetActive(false);

        //implement middle block positioning

        if(getStep() == 6)
        {
            incrementStep();
            setCompletedStatus(true);
            //topOfTower.SetActive(false);
        }
    }

    public void unKinematic(int pieces)
    {
        Debug.Log("Count of Jenga Pieces: " + pieces);
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

    public int getStep()
    {
        return stepNum;
    }

    public bool getStepStatus()
    {
        return completedStep;
    }

    public void setCompletedStatus(bool status)
    {
        completedStep = status;
    }

    public void incrementStep()
    {
        stepNum++;
        Debug.Log("Step number: " + stepNum);
    }
}

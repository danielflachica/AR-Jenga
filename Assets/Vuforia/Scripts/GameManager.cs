using System.Collections;
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
    private bool isTethered = false;
    private BlockInteraction interactor;
    private Vector3 offset;

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
    }

    // Update is called once per frame
    void Update()
    {

        if (isTethered)
        {
            Debug.Log("Tethered block!");

            // Distance moved = time * speed.
            float distCovered = (Time.time - startTime) * speed;

            // Fraction of journey completed = current distance divided by total distance.
            float fracJourney = distCovered / journeyLength;

            offset = arCamera.transform.localPosition - block.transform.localPosition;

            // Set our position as a fraction of the distance between the markers.
            transform.position = Vector3.Lerp(arCamera.transform.position, block.transform.position + offset, fracJourney);

            /*Debug.Log("camera position: " + arCamera.transform.localPosition);
            offset = arCamera.transform.localPosition - block.transform.localPosition;
            Debug.Log("block old position: "+ block.transform.localPosition);
            block.transform.localPosition = arCamera.transform.localPosition + offset;
            Debug.Log("block new position: " + block.transform.localPosition);*/
        }

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
                    block.GetComponent<Rigidbody>().useGravity = false;
                    block.GetComponent<Rigidbody>().isKinematic = true;
                    Debug.Log("Touch Detected on block: " + block.name);

                    interactor.highlightObject(block);
                    isTethered = true;
                    enableCanvas();

                    // Keep a note of the time the movement started.
                    startTime = Time.time;

                    // Calculate the journey length.
                    journeyLength = Vector3.Distance(arCamera.transform.position, block.transform.position);
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

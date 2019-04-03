using System.Collections;
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
    public GameObject gameOverPanel;
    public Button releaseBtn;
    public Button rotateBtn;
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
    public bool exploding; //game mode
    public GameObject explosion;
    public bool drinking; //game mode
    private string[] drinkingTitles;
    private string[] drinkingActions;
    public GameObject drinkingPanel;
    public Text txtDrinkingTitle;
    public Text txtDrinkingAction;
    public Button btnCloseDrinking;
    private bool gameOver;

    // Time when the movement started.
    private float startTime;

    // Total distance between the markers.
    private float journeyLength;

    // Movement speed in units/sec.
    public float speed = 1.0F;


    // Start is called before the first frame update
    void Start()
    {
        gameOver = false;
        interactor = new BlockInteraction();
        releaseBtn.onClick.AddListener(releaseBlock);
        rotateBtn.onClick.AddListener(rotateBlock);
        topStackCount = 0;
        ss = new ScoringScript();
        dropZone = false;
        block = null;

        if(exploding) //means exploding is enabled
        {
            int bombCount = Random.Range(1,tower.transform.childCount),bombIndex;

            while(bombCount > 0) //assign bomb tag
            {
                bombIndex = Random.Range(0,tower.transform.childCount);

                tower.transform.GetChild(bombIndex).tag = "bomb";
                Debug.Log("Bomb tag at block: "+tower.transform.GetChild(bombIndex).name);

                bombCount--;
            }
        }

        if(drinking) //drinking mode enabled
        {
            Debug.Log("Drinking mode enabled!");

            drinkingTitles = new string[10];
            drinkingActions = new string[10];
            btnCloseDrinking.onClick.AddListener(CloseDrinkingPanel);

            InitDrinkingMode();
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (gameOver)
            gameOverPanel.SetActive(true);
        else
        {
            if (isTethered && block != null)
            {
                //Debug.Log("Tethered block!");

                // Distance moved = time * speed.
                float distCovered = (Time.time - startTime) * speed;

                // Fraction of journey completed = current distance divided by total distance.
                float fracJourney = distCovered / journeyLength;

                // Set our position as a fraction of the distance between the markers.
                block.transform.position = Vector3.Lerp(block.transform.position, arCamera.transform.position - offset, fracJourney);

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
                    if (hit.collider.gameObject.tag == "jengaBlock" || hit.collider.gameObject.tag == "bomb")
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

                    if (hit.transform.gameObject.tag == "bomb") //explode block and destory object
                    {
                        Instantiate(explosion, block.transform.position, block.transform.rotation, tower.transform);
                        Destroy(block);
                        block = null;
                        blockControlPanel.SetActive(false);
                        isTethered = false;
                        interactor.setStatus(false);

                        gameOver = true;
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
        }
        

        scorePanel.text = "Score: "+ss.getScore();


    }

    public void setDropStatus(bool stat)
    {
        dropZone = stat;
    }

    public void releaseBlock()
    {
        if (dropZone) // if the block was dropped at the top of the stack
        {
            ss.Score();
            incrementTopStackCount();

            //display dare
            int i = Random.Range(0, 10); //random index from 0-9 (10 drinking dares/truths)
            txtDrinkingTitle.text = drinkingTitles[i];
            txtDrinkingAction.text = drinkingActions[i];
            drinkingPanel.SetActive(true);
        }

        block.GetComponent<Rigidbody>().useGravity = true;
        block.GetComponent<Rigidbody>().isKinematic = false;
        isTethered = false;
        disableBlockControlPanel();
        //canvas.SetActive(false);
        interactor.unHighlightObject(block);
        block.GetComponent<Rigidbody>().mass = 3;
        //block = null;

        setDropStatus(false);
    }

    public void InitDrinkingMode()
    {
        drinkingTitles[0] = "Truth";
        drinkingTitles[1] = "Truth";
        drinkingTitles[2] = "Truth";
        drinkingTitles[3] = "Truth";
        drinkingTitles[4] = "Truth";
        drinkingTitles[5] = "Dare";
        drinkingTitles[6] = "Dare";
        drinkingTitles[7] = "Dare";
        drinkingTitles[8] = "Dare";
        drinkingTitles[9] = "Dare";

        drinkingActions[0] = "Who in the room do you think would be a bad date?";
        drinkingActions[1] = "What physical feature do you get complimented on the most?";
        drinkingActions[2] = "If you were the opposite sex for one hour, what would you do?";
        drinkingActions[3] = "Have you ever thought of cheating on your boyfriend/girlfriend?";
        drinkingActions[4] = "Lights on or lights off?";
        drinkingActions[5] = "Do the Dalagang Pilipina challenge.";
        drinkingActions[6] = "Dance the Kahit Ayaw Mo Na challenge.";
        drinkingActions[7] = "Give a foot massage to anyone in the room.";
        drinkingActions[8] = "Let someone write a word on your forehead in permanent marker.";
        drinkingActions[9] = "Hug someone intimately for 10 seconds.";
    }

    public void CloseDrinkingPanel()
    {
        drinkingPanel.SetActive(false);
    }
    
    public void rotateBlock()
    {
        block.transform.Rotate(Vector3.up, 50f * Time.deltaTime);
    }

    public void incrementTopStackCount()
    {
        Debug.Log("incremented stack count!");

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

    public bool getGameStatus()
    {
        return gameOver;
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockToGroundCollision : MonoBehaviour
{

    public GameObject canvas;
    public GameObject losePanel;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Something hit me!!");

        canvas.SetActive(true);
        losePanel.SetActive(true);
    }


}

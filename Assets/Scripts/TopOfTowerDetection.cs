using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopOfTowerDetection : MonoBehaviour
{

    public TutorialManager gm;

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

        if(gm.getStep() == 5 && other.name == gm.block.name)
        {
            gm.setCompletedStatus(true);
            gm.incrementStep();
        }
    }
}

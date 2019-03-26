using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialPanel : MonoBehaviour
{
    public GameObject intro, towerPlace, jengaSelect, jengaPull, jengaMove, jengaDrop, complete;
    public Button introBtn, completeBtn;
    public TutorialManager gm;
    private bool panelActive = false; //a tutorial step g.o. hasnt been shown yet
    private bool completedStep;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        completedStep = gm.getStepStatus();

        if (completedStep && panelActive)
        {
            switch (gm.getStep() - 1)
            {
                case 1:
                    intro.SetActive(false);
                    panelActive = false;
                    gm.setCompletedStatus(false);
                    break;
                case 2:
                    towerPlace.SetActive(false);
                    panelActive = false;
                    gm.setCompletedStatus(false);
                    break;
                case 3:
                    jengaSelect.SetActive(false);
                    panelActive = false;
                    gm.setCompletedStatus(false);
                    break;
                case 4:
                    jengaPull.SetActive(false);
                    panelActive = false;
                    gm.setCompletedStatus(false);
                    break;
                case 5:
                    jengaMove.SetActive(false);
                    panelActive = false;
                    gm.setCompletedStatus(false);
                    break;
                case 6:
                    jengaDrop.SetActive(false);
                    panelActive = false;
                    gm.setCompletedStatus(false);
                    break;
                case 7:
                    complete.SetActive(false);
                    panelActive = false;
                    gm.setCompletedStatus(false);
                    break;
            }
        }

        if (panelActive == false)
        {
            //Debug.Log("Tutorial Panel Script @ !panelActive");
            Debug.Log("Step num: " + gm.getStep());
            switch (gm.getStep())
            {
                case 1:
                    intro.SetActive(true);
                    panelActive = true;
                    completedStep = false;
                    break;
                case 2:
                    towerPlace.SetActive(true);
                    panelActive = true;
                    completedStep = false;
                    break;
                case 3:
                    jengaSelect.SetActive(true);
                    panelActive = true;
                    completedStep = false;
                    break;
                case 4:
                    jengaPull.SetActive(true);
                    panelActive = true;
                    completedStep = false;
                    break;
                case 5:
                    jengaMove.SetActive(true);
                    panelActive = true;
                    completedStep = false;
                    break;
                case 6:
                    jengaDrop.SetActive(true);
                    panelActive = true;
                    completedStep = false;
                    break;
                case 7:
                    complete.SetActive(true);
                    panelActive = true;
                    completedStep = false;
                    break;
            }
        }

    
    }

}

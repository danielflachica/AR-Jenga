using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialTopOfTowerMechanic : MonoBehaviour
{
    public GameObject topOfTower;
    public GameObject topBlockPos;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position =   new Vector3(topBlockPos.transform.position.x,topBlockPos.transform.position.y + 0.110f,topBlockPos.transform.position.z);
    }
}

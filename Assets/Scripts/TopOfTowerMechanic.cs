using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopOfTowerMechanic : MonoBehaviour
{

    public GameManager gm;

    public GameObject topBlockPos;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (gm.getTopStackCount() == 2)
            topBlockPos = gm.block;

        transform.position = new Vector3(topBlockPos.transform.position.x, topBlockPos.transform.position.y + 0.110f, topBlockPos.transform.position.z);
    }

    private void OnTriggerEnter(Collider other)
    {


    }


}

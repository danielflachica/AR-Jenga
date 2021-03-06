﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopOfTowerMechanic : MonoBehaviour
{

    public GameManager gm;

    public GameObject topBlockPos;

    private bool dropZone;

    // Start is called before the first frame update
    void Start()
    {
        dropZone = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (gm.getTopStackCount() == 2)
        {

            topBlockPos = gm.block;
        }

        transform.position = new Vector3(topBlockPos.transform.position.x, topBlockPos.transform.position.y + 0.150f, topBlockPos.transform.position.z);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Collided with: " + other.name);
        if(gm.block != null)
            if(other.name == gm.block.name)
                gm.setDropStatus(true);
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnTouchScript : MonoBehaviour
{
    public GameObject explosion;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Explosion!");    
    }
}

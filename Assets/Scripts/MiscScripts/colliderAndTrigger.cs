using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class colliderAndTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("ALgo entro en trigger "+other.name);
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collision con "+collision.gameObject.name);
        
    }
}

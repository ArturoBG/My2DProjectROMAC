using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("collision entered " + collision.name);
        if (collision.tag == "playerWeapons")
        {
            Debug.Log("damage!");
        }
    }
}
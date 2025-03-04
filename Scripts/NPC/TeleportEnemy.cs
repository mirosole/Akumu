using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportEnemy : MonoBehaviour
{
    public Transform destinationTrigger; // Reference to the destination trigger
    public bool isTeleport;

    private void OnTriggerEnter(Collider other)
    {
        if (isTeleport)
        {
            if (other.CompareTag("Enemy")) // Check if the entering object is the NPC
            {
                Debug.Log("NPC entered trigger, transporting to destination.");
                other.transform.position = destinationTrigger.position; // Move NPC to destination
            }
        }
    }
    
    #if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawCube(transform.position, transform.localScale);
    }
    #endif
}

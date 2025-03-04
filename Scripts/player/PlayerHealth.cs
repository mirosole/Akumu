using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    private bool safeSpace = false;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("SafeSpace"))
        {
            safeSpace = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("SafeSpace"))
        {
            safeSpace = false;
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            // Check if the enemy is currently attacking
            Enemy enemy = other.gameObject.GetComponent<Enemy>();
            if (enemy != null && enemy.isAttacking && !safeSpace)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                Debug.Log("Collided with an attacking enemy!");
                SceneManager.LoadScene("DeathScreen");
            }
        }
    }
}

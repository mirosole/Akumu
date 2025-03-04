using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TriggerCutscene : MonoBehaviour
{
    [SerializeField] private PickUpObject pickUpScript;
    
    // public GameObject key; // Reference to the key GameObject
    private bool playerInTrigger = false; // Tracks if the player is in the trigger
    private bool keyHeld = false; // Tracks if the key is being held by the player

    private void Update()
    {
        GameObject currentObject = pickUpScript.CurrentObject;
        if (currentObject != null && currentObject.CompareTag("PickableObject"))
        {
            Key key = currentObject.GetComponent<Key>();
            if (key != null && key.GetKeyID() == 100)
            {
                keyHeld = true;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInTrigger = true; // Player entered the trigger
        }

        // Trigger the cutscene if the player is in the trigger and the key is either in the trigger or held
        if (playerInTrigger && keyHeld)
        {
            TriggerCutsceneEvent();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInTrigger = false;
        }
    }

    private void TriggerCutsceneEvent()
    {
        SceneManager.LoadScene("FinalCutscene");
    }

#if UNITY_EDITOR
    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(transform.position, transform.lossyScale);
    }
#endif
}
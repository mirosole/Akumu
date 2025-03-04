using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private bool requiresKey = true; // Determines if the door requires a key to open
    [SerializeField] private int requiredKeyID; // ID of the key that can open this door
    [SerializeField] private Animator animator; // Animator component for door animations
    private bool isUnlocked = false; // Flag whether the door is unlocked
    private bool isOpen = false; // Door status: open or closed

    // Method to unlock the door using a key
    public bool TryUnlock(Key key)
    {
        if (isOpen)
        {
            Debug.Log("The door is already open.");
            return false;
        }

        if (!requiresKey)
        {
            OpenDoor();
            return true;
        }

        int keyID = key.GetKeyID();

        if (!isUnlocked && keyID == requiredKeyID)
        {
            isUnlocked = true;
            OpenDoor();
            return true;
        }
        else if (isUnlocked)
        {
            Debug.Log("The door is already unlocked.");
            OpenDoor();
            return true;
        }
        else
        {
            Debug.Log("This key does not match the door's lock.");
            return false;
        }
    }

    // Method to toggle door state without a key
    public void ToggleDoor()
    {
        if (isOpen)
        {
            CloseDoor();
        }
        else
        {
            TryOpenWithoutKey();
        }
    }

    // Method to open the door without a key
    public void TryOpenWithoutKey()
    {
        if (requiresKey && !isUnlocked)
        {
            Debug.Log("This door is locked. Find a key to open it.");
            return;
        }

        OpenDoor();
    }

    // Internal method to handle door opening
    private void OpenDoor()
    {
        isOpen = true;
        Debug.Log("The door is now open.");
        if (animator != null)
        {
            animator.SetBool("IsOpen", true); // Set the IsOpen parameter to true
        }
    }

    // Internal method to handle door closing
    private void CloseDoor()
    {
        isOpen = false;
        Debug.Log("The door is now closed.");
        if (animator != null)
        {
            animator.SetBool("IsOpen", false); // Set the IsOpen parameter to false
        }
    }
}

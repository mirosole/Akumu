using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseKey : MonoBehaviour
{
    [SerializeField] private PickUpObject pickUpScript;
    [SerializeField] private Camera playerCamera;

    private float useDistance = 3f;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            bool usedKeySuccessfully = TryUseKey();
            if (!usedKeySuccessfully)
            {
                TryOpenOrCloseDoor();
            }
        }
    }

    bool TryUseKey()
    {
        GameObject currentObject = pickUpScript.CurrentObject;

        if (currentObject != null && currentObject.CompareTag("PickableObject"))
        {
            Key key = currentObject.GetComponent<Key>();
            if (key != null)
            {
                RaycastHit[] hits = Physics.RaycastAll(playerCamera.transform.position, playerCamera.transform.forward, useDistance);
                foreach (RaycastHit hit in hits)
                {
                    Door door = hit.transform.GetComponent<Door>();
                    if (door != null)
                    {
                        return door.TryUnlock(key);
                    }
                }
            }
        }

        return false; // Key was not used successfully
    }

    // Method to try opening or closing doors without a key using RaycastAll
    void TryOpenOrCloseDoor()
    {
        RaycastHit[] hits = Physics.RaycastAll(playerCamera.transform.position, playerCamera.transform.forward, useDistance);
        foreach (RaycastHit hit in hits)
        {
            Door door = hit.transform.GetComponent<Door>();
            if (door != null)
            {
                door.ToggleDoor();
                break; // Exit the loop after toggling one door
            }
        }
    }
}

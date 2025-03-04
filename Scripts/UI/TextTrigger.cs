using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextTrigger : MonoBehaviour
{
    public String textToDisplay;
    private TasksManager tasksManager;

    public bool Reusable = false;
    private bool triggered = false;
    // Start is called before the first frame update
    void Start()
    {
        tasksManager = FindObjectOfType<TasksManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (tasksManager != null)
            {
                if (Reusable)
                {
                    tasksManager.UpdateText(textToDisplay);
                }
                else if (!triggered)
                {
                    tasksManager.UpdateText(textToDisplay);
                    triggered = true;
                }
        }
            else
            {
                Debug.LogWarning("No TasksManager found in the scene!");
            }
        }
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position, transform.lossyScale);
    }
#endif
}

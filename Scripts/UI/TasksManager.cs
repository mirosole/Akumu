using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TasksManager : MonoBehaviour
{
    public TextMeshProUGUI taskText;
    // Start is called before the first frame update
    void Start()
    {
        taskText.text = "";
    }

    public void UpdateText(String newText)
    {
        taskText.text = newText;
        taskText.enableAutoSizing = true;
    }
}

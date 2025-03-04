using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour
{
    [SerializeField] private float durationTime;
    [SerializeField] private float timeBeforeActivation;
    [SerializeField] private Screamer screamer;
    private bool trigerHadBeenActivated;

    void Start()
    {
        trigerHadBeenActivated = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !trigerHadBeenActivated)
        {
            //trigerHadBeenActivated = true;
            StartScreamer();
        }
    }

    public void StartScreamer()
    {
        if (!trigerHadBeenActivated)
            StartCoroutine(screamer.Activate(timeBeforeActivation, durationTime));

        trigerHadBeenActivated = true;
    }
}


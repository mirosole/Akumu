using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEngine;
using UnityEngine.Playables;

public class Quest4CutScene : MonoBehaviour
{
    [SerializeField] private  PlayerMovement playerMovement;
    [SerializeField] private PlayableDirector playableDirector;
       
    TriggerCutsceneOnPickUp triggerCutsceneOnPickUp;
    private bool wasPlayed; 

    void Start()
    {
        wasPlayed = false;
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player" && !wasPlayed)
        {

        }
    }
}

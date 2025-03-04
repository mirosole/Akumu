using System;

using UnityEngine;
using Random = UnityEngine.Random;

public class FootSteps : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip[] footSteps;
    [SerializeField] private float walkStepInterval = 0.5f;
    [SerializeField] private float runStepInterval = 0.3f;

    private PlayerMovement playerMovement;
    private Rigidbody rb;
    [SerializeField] private float stepTimer;
    private float stepInterval;
    private bool isMoving;

    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        rb = GetComponent<Rigidbody>();
        stepTimer = 0f;
    }

    void Update()
    {
        isMoving = rb.velocity.magnitude > 0.1f;
        
        if (isMoving && !playerMovement.isPlayerFalling())
        {
            stepInterval = playerMovement.isPlayerRunning() ? runStepInterval : walkStepInterval;
            stepTimer += Time.deltaTime;
            if (stepTimer >= stepInterval)
            {
                PlayFootstep();
                stepTimer = 0f;
            }
        }
        else
        {
            stepTimer = 0f;
        }

    }

    private void PlayFootstep()
    {
        AudioClip[] footStepSounds = footSteps;
        AudioClip audioClip = footStepSounds[Random.Range(0, footStepSounds.Length)];
        audioSource.pitch = (Random.Range(0.95f, 1.15f));
        audioSource.PlayOneShot(audioClip);
    }
    
}

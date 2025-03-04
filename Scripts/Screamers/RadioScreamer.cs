using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadioScreamer : MonoBehaviour
{
    private bool wasPlayed;
    private bool isPlaying;
    private AudioSource audioSource;
    private float currentPitch;

    [SerializeField] private float playTime = 5f; 

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        isPlaying = false;
        wasPlayed = false;
        currentPitch = audioSource.pitch;
    }

    void Update()
    {
        if (isPlaying)
        {
            currentPitch = currentPitch + Random.Range(-0.05f, 0.05f);
            currentPitch = Mathf.Clamp(currentPitch, -2f, 2f);
            audioSource.pitch = currentPitch;
        }
    }

    private void playRadio()
    {
        if (!isPlaying)
        {
            StartCoroutine(PlaySoundForDuration(playTime));
        }
    }

    private IEnumerator PlaySoundForDuration(float duration)
    {
        audioSource.Play();
        isPlaying = true;
        wasPlayed = true;

        yield return new WaitForSeconds(duration); 

        audioSource.Stop();
        isPlaying = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !wasPlayed)
        {
            Debug.Log("Radio is playing");
            playRadio();
        }
    }
}

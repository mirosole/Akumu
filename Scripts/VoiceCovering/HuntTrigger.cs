using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HuntTrigger : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    private bool hasPlayed = false;

    private bool HasPlayed() => hasPlayed;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !hasPlayed)
        {
            if (audioSource != null && audioSource.clip != null)
            {
                StartCoroutine(Wait(2f));
                audioSource.Play();
                hasPlayed = true;
            }
        }
    }

    private IEnumerator Wait(float time)
    {
        yield return new WaitForSeconds(time);
    }
}
using System.Collections;
using UnityEngine;

public class LampBlinking : MonoBehaviour
{
    [SerializeField] private Light[] lamp;
    private AudioSource audioSource;
    private bool inRange;
    private Coroutine blinkCoroutine;
    public const float blinkDuration = 0.2f;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        inRange = false;
        foreach (Light light in lamp)
        {
            light.enabled = true;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !inRange)
        {
            inRange = true;
            if (audioSource != null)
            {
                audioSource.Play();
            }
            blinkCoroutine = StartCoroutine(Blink());
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && inRange)
        {
            inRange = false;
            if (blinkCoroutine != null)
            {
                StopCoroutine(blinkCoroutine);
            }

            if (audioSource != null)
            {
                audioSource.Stop();
            }

            foreach (Light light in lamp)
            {
                light.enabled = true;
            }
        }
    }

    private IEnumerator Blink()
    {
        while (inRange)
        {
            yield return new WaitForSeconds(Random.Range(0, blinkDuration));
            foreach (Light light in lamp)
            {
                light.enabled = !light.enabled;
            }
        }
    }
}

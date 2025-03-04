using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Serialization;

public class LightRoad : MonoBehaviour
{
    [SerializeField] private AudioSource[] audioSources;
    [SerializeField] private Light[] lights;
    [SerializeField] private float interval;
    [SerializeField] private Door door;
    private bool wasPlayed;

    public void StartLight()
    {
        
        door.TryOpenWithoutKey();
        StartCoroutine(TurnLightOn());
    }

    private IEnumerator TurnLightOn()
    {
        if (!wasPlayed)
        {
            wasPlayed = true;
            for (int i = 0; i < lights.Length; i++)
            {
                yield return new WaitForSeconds(interval);
                lights[i].enabled = true;
                audioSources[i].enabled = true;
                audioSources[i].Play();
            }
        }
    }
}

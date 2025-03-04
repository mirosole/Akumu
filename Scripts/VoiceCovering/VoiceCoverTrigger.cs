using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoiceCoverTrigger : MonoBehaviour
{

    [SerializeField] private int phraseNumber;
    private VoiceCovererHandler voiceCovererHandler;
    private bool wasPlayed = false;
    

    void Start()
    {
        voiceCovererHandler = FindFirstObjectByType<VoiceCovererHandler>();
    }


    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(voiceCovererHandler.lastPlayed);

        if (other.CompareTag("Player") && !wasPlayed && voiceCovererHandler.lastPlayed == phraseNumber - 1) // make them play consistently  
        {
            Debug.Log("Im in");
            wasPlayed = true;
            voiceCovererHandler.playClip(phraseNumber);
            voiceCovererHandler.lastPlayed++;
        }
    }


#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireCube(transform.position, transform.lossyScale);
    }
#endif

}


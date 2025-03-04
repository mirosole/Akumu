using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoiceCovererHandler : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioListener audioListener;
    private AudioClip[] audioClip;
    public int lastPlayed;

    void Start()
    {
        LoadAudioClips();
    }

    public void playClip(int number)
    {
        if (number >= 0 && number < audioClip.Length)
        {
            audioSource.Stop();
            audioSource.clip = audioClip[number];
            audioSource.Play();
            Debug.Log(audioClip[number].name + " " + number);
            Debug.Log(audioClip.Length);

        }
    }


    private void LoadAudioClips()
    {
        audioClip = Resources.LoadAll<AudioClip>("Dialogs");

        if (audioClip.Length > 0)
        {
            System.Array.Sort(audioClip, (clip1, clip2) =>
            {
                int num1 = ExtractNumberFromName(clip1.name);
                int num2 = ExtractNumberFromName(clip2.name);
                return num1.CompareTo(num2);
            });

            Debug.Log($"{audioClip.Length} success");
        }
        else
        {
            Debug.LogWarning("unable to detect Resources/Dialogs.");
        }
    }

    private int ExtractNumberFromName(string name)
    {
        int number;
        if (int.TryParse(name, out number))
        {
            return number;
        }

        Debug.LogWarning($"Unable to convert '{name}' in number.");
        return 0; 
    }



}

using Cinemachine;
using UnityEngine;
using UnityEngine.Playables;

public class TriggerCutsceneOnPickUp : MonoBehaviour
{
    [SerializeField] private PlayableDirector cutsceneDirector;
    [SerializeField] private PlayerMovement playerMovement;
    private GameObject cutScene;

    private bool wasPlayed = false;
    private bool cutscenePlaying = false;
    
    public void OnPickUp()
    {
        if (cutsceneDirector != null && !cutscenePlaying && !wasPlayed)
        {
            Debug.Log("First validation");
            cutScene = cutsceneDirector.gameObject.transform.parent.gameObject;            
            OnsrartCutsceneStart();
        }
    }

    private void OnsrartCutsceneStart()
    {
        wasPlayed = true;
        cutscenePlaying = true;


        playerMovement.BlockMovemant = true;
        playerMovement.BlockRotation = true;

        cutsceneDirector.stopped += OnCutsceneEnd;
        cutsceneDirector.Play();
        Debug.Log("OnsrartCutsceneStart");

    }

    private void OnCutsceneEnd(PlayableDirector director)
    {

        playerMovement.BlockMovemant = false;
        playerMovement.BlockRotation = false;

        cutscenePlaying = false;

        cutsceneDirector.stopped -= OnCutsceneEnd;
        Debug.Log("OnCutsceneEnd");
        Destroy(cutScene);
    }
}

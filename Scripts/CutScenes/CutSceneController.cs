using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Playables;

public class CutSceneController : MonoBehaviour
{
    private bool wasPlayed;
    private Transform lastCameraTransforml;
    private GameObject Player;

    [SerializeField] private bool movePlayerOnTheLastCameraPosition;
    [SerializeField] private bool TurnOffHandObject;
    [SerializeField] private GameObject handObject;
    [SerializeField] private PlayableDirector PlayableDirector;
    [SerializeField] private PlayerMovement PlayerMovement;
    [SerializeField] private GameObject cutScene;

    void Start()
    {

        wasPlayed = false;
        if (movePlayerOnTheLastCameraPosition)
        {
            Player = GameObject.FindWithTag("Player");
            lastCameraTransforml = GameObject.Find("LastCamera").transform;

        }

    }

    public void OnPickUp()
    {
        if (PlayableDirector != null && !wasPlayed)
        {
            Debug.Log("First validation");
            OnsrartCutsceneStart();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !wasPlayed)
        {
            Debug.Log("Cutscene started");


            OnsrartCutsceneStart();

        }
    }

    internal void OnsrartCutsceneStart()
    {
        wasPlayed = true;

        if (TurnOffHandObject)
            SetVisibility(handObject, false);

        PlayerMovement.BlockMovemant = true;
        PlayerMovement.BlockRotation = true;

        PlayableDirector.stopped += OnCutsceneEnd;
        PlayableDirector.Play();
        Debug.Log("OnsrartCutsceneStart");

    }

    private void OnCutsceneEnd(PlayableDirector director)
    {

        PlayerMovement.BlockMovemant = false;
        PlayerMovement.BlockRotation = false;

        if (TurnOffHandObject)
            SetVisibility(handObject, true);

        if (movePlayerOnTheLastCameraPosition)
        {
            Player.transform.rotation = lastCameraTransforml.rotation;
            Player.transform.position = new Vector3(lastCameraTransforml.position.x, Player.transform.position.y, lastCameraTransforml.position.z);
        }

        PlayableDirector.stopped -= OnCutsceneEnd;
        Debug.Log("OnCutsceneEnd");
       
        //if (cutScene != null)
        //    Destroy(cutScene);
    }


    private void SetVisibility(GameObject targetObject, bool isVisible)
    {
        Renderer[] renderers = targetObject.GetComponentsInChildren<Renderer>();
        foreach (Renderer renderer in renderers)
        {
            renderer.enabled = isVisible;
        }
    }

}

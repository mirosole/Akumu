using System.Collections;
using UnityEngine;

public class Screamer : MonoBehaviour
{
    private bool playerCollider = false;
    private Coroutine activeCoroutine;

    public float moveSpeed = 5f; 
    private bool isActive = false; 
    private bool hasWaited = false;

    public IEnumerator Activate(float timeBeforeActivation, float durationTime)
    {
        yield return new WaitForSeconds(timeBeforeActivation);
        if (playerCollider) yield break;
        gameObject.SetActive(true);
        yield return new WaitForSeconds(2f);
        hasWaited = true;

        Debug.Log("Screamer activated!");

        isActive = true; 

        activeCoroutine = StartCoroutine(DeactivateAfterTime(durationTime));
    }

    private IEnumerator DeactivateAfterTime(float duration)
    {
        yield return new WaitForSeconds(duration);
        Deactivate();
    }

    private void Deactivate()
    {
        if (activeCoroutine != null)
        {
            StopCoroutine(activeCoroutine);
            activeCoroutine = null;
        }

        isActive = false; 
        gameObject.SetActive(false);
        playerCollider = false;
        Destroy(gameObject.transform.parent.gameObject);
        Debug.Log("Screamer has been deactivated");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!playerCollider && other.CompareTag("Player"))
        {
            Debug.Log("Player ran into the screamer");
            playerCollider = true;
            Deactivate();
        }
    }

    private void Update()
    {
        if (isActive && hasWaited)
        {
            transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
        }
    }
}

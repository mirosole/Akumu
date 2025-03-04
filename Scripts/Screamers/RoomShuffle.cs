using UnityEngine;

public class FurnitureSwitcher : MonoBehaviour
{
    [SerializeField] private GameObject furniture;
    [SerializeField] private GameObject furnitureScreamer;
    [SerializeField] private AudioSource crashSound;
    private bool hasTriggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (hasTriggered) return;

        if (other.CompareTag("Player")) 
        {
            hasTriggered = true; 

            if (furniture != null)
                furniture.SetActive(false);

            if (furnitureScreamer != null)
                furnitureScreamer.SetActive(true);

            if (crashSound != null)
                crashSound.Play();
        }
    }
}
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro; 
using UnityEngine.SceneManagement; 

public class SceneMNG : MonoBehaviour
{
    public Image[] images; // Array of images to switch through
    public float switchTime = 30f; // Time in seconds to switch between images
    public float fadeDuration = 2f; // Duration of fade effect
    public TextMeshProUGUI endMessage; // Text to display at the end
    public TextMeshProUGUI anyKey;
    private int currentIndex = 0; // Index of the currently active image

    // Start is called before the first frame update
    void Start()
    {
        if (images.Length > 0)
        {
            // Ensure only the first image is active at the start, but all have zero alpha
            foreach (var image in images)
            {
                SetImageAlpha(image, 0);
                image.gameObject.SetActive(true); // Activate all to allow alpha changes
            }

            if (endMessage != null)
            {
                endMessage.gameObject.SetActive(false); // Hide the message initially
                anyKey.gameObject.SetActive(false);
            }

            // Start showing the first image
            StartCoroutine(FadeIn(images[currentIndex]));
            StartCoroutine(SwitchImages());
        }
        else
        {
            Debug.LogWarning("No images assigned to the SceneMNG script!");
        }
    }

    private IEnumerator SwitchImages()
    {
        while (currentIndex < images.Length - 1) // Loop through the images
        {
            yield return new WaitForSeconds(switchTime);

            // Fade out the current image
            yield return FadeOut(images[currentIndex]);

            // Move to the next image and fade it in
            currentIndex++;
            yield return FadeIn(images[currentIndex]);
        }

        // After the last image, fade it out and hide all images
        yield return new WaitForSeconds(switchTime);
        yield return FadeOut(images[currentIndex]);

        foreach (var image in images)
        {
            SetImageAlpha(image, 0); // Ensure all images are fully transparent
            image.gameObject.SetActive(false); // Optionally deactivate all images
        }

        // Show the end message
        if (endMessage != null && anyKey != null)
        {
            endMessage.gameObject.SetActive(true);
            anyKey.gameObject.SetActive(true);
            StartCoroutine(WaitForAnyKey());
        }
    }

    private IEnumerator WaitForAnyKey()
    {
        while (!Input.anyKeyDown) // Wait until any key is pressed
        {
            yield return null;
        }

        // Load the menu scene
        SceneManager.LoadScene("Main Menu Scene");
    }

    private IEnumerator FadeIn(Image image)
    {
        float elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Clamp01(elapsedTime / fadeDuration);
            SetImageAlpha(image, alpha);
            yield return null;
        }
        SetImageAlpha(image, 1); // Ensure alpha is fully set
    }

    private IEnumerator FadeOut(Image image)
    {
        float elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Clamp01(1 - (elapsedTime / fadeDuration));
            SetImageAlpha(image, alpha);
            yield return null;
        }
        SetImageAlpha(image, 0); // Ensure alpha is fully set
    }

    private void SetImageAlpha(Image image, float alpha)
    {
        if (image != null)
        {
            Color color = image.color;
            color.a = alpha;
            image.color = color;
        }
    }
}

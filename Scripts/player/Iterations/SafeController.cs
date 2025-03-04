using UnityEngine;
using UnityEngine.UI;

public class SafeController : MonoBehaviour
{
    [Header("Safe Configuration")]
    [SerializeField] private string correctPassword = "1234"; // Correct password
    [SerializeField] private Animator safeAnimator; // Door animator

    [Header("UI Elements")]
    [SerializeField] private GameObject panel; // UI panel with input
    [SerializeField] private InputField passwordInputField; // Input field

    [Header("Player Interaction")]
    [SerializeField] private float interactionDistance = 3f;
    [SerializeField] private LayerMask interactableLayer;

    [Header("References")]
    [SerializeField] private PlayerMovement playerMovement; // Reference to player movement script

    private bool isSafeOpen = false;
    private bool isLookingAtSafe = false;

    void Start()
    {
        panel.SetActive(false);
    }

    void Update()
    {
        CheckPlayerLookingAtSafe();

        // Open panel if player interacts
        if (Input.GetKeyDown(KeyCode.F) && isLookingAtSafe && !panel.activeSelf && !isSafeOpen)
        {
            OpenInputPanel();
        }

        // Submit password on Enter
        if (panel.activeSelf && Input.GetKeyDown(KeyCode.Return))
        {
            CheckPassword();
        }

        // Close panel on Escape
        if (panel.activeSelf && Input.GetKeyDown(KeyCode.Escape) && !isSafeOpen)
        {
            CloseInputPanel();
        }
    }

    private void CheckPlayerLookingAtSafe()
    {
        RaycastHit hit;
        isLookingAtSafe = Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, interactionDistance, interactableLayer)
                          && hit.collider.gameObject == this.gameObject;
    }

    private void OpenInputPanel()
    {
        panel.SetActive(true);
        passwordInputField.text = ""; // Clear previous input
        passwordInputField.ActivateInputField();

        // Disable player movement and lock controls
        playerMovement.BlockMovemant = true;
        playerMovement.BlockRotation = true;

        // Show cursor for input
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private void CloseInputPanel()
    {
        panel.SetActive(false);

        // Re-enable player movement and controls
        playerMovement.BlockMovemant = false;
        playerMovement.BlockRotation = false;

        // Hide cursor for gameplay
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void CheckPassword()
    {
        string enteredPassword = passwordInputField.text;

        if (enteredPassword == correctPassword)
        {
            Debug.Log("Correct password! Opening safe...");
            OpenSafeDoor();
        }
        else
        {
            Debug.Log("Incorrect password. Try again.");
            passwordInputField.text = ""; // Clear field
            passwordInputField.ActivateInputField(); // Focus input
        }
    }

    private void OpenSafeDoor()
    {
        if (!isSafeOpen)
        {
            isSafeOpen = true;
            CloseInputPanel();
            safeAnimator.SetBool("IsOpen", true); // Play door animation
        }
    }

    public bool IsSafeOpen()
    {
        return isSafeOpen;
    }
}

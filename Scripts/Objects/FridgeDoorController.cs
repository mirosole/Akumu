using UnityEngine;

public class FridgeDoorController : MonoBehaviour
{
    [SerializeField] private Transform doorTransform;
    [SerializeField] private float openAngle = -90f;
    [SerializeField] private float speed = 2f;
    [SerializeField] private float interactionDistance = 3f;

    private bool isOpen = false;
    private Quaternion closedRotation;
    private Quaternion openRotation;

    private Camera playerCamera;
    private bool isLookingAtDoor = false;

    void Start()
    {
        closedRotation = doorTransform.localRotation;
        openRotation = closedRotation * Quaternion.Euler(0, openAngle, 0);

        playerCamera = Camera.main;
    }

    void Update()
    {
        CheckPlayerLookingAtDoor();

        if (isLookingAtDoor && Input.GetKeyDown(KeyCode.F))
        {
            isOpen = !isOpen;
        }

        doorTransform.localRotation = Quaternion.Slerp(
            doorTransform.localRotation,
            isOpen ? openRotation : closedRotation,
            Time.deltaTime * speed
        );
    }

    private void CheckPlayerLookingAtDoor()
    {
        Ray ray = playerCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, interactionDistance))
        {
            if (hit.transform == doorTransform)
            {
                isLookingAtDoor = true;
                return;
            }
        }

        isLookingAtDoor = false;
    }
}

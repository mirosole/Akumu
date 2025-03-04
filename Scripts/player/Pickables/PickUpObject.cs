using UnityEngine;

public class PickUpObject : MonoBehaviour
{
    [SerializeField] private Camera playerCamera; // Player's camera for raycasting
    private float pickUpDistance = 5f; // Max distance to pick up objects
    private GameObject currentObject;
    public GameObject CurrentObject => currentObject;
    private int originalLayer; // To store the object's original layer

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) PickUp();
        if (Input.GetKeyDown(KeyCode.G)) Drop();
    }

    void PickUp()
    {
        if (currentObject != null) return; // Exit if already holding something

        RaycastHit[] hits = Physics.RaycastAll(playerCamera.transform.position, playerCamera.transform.forward, pickUpDistance);
        GameObject pickableObject = null;

        foreach (var hit in hits)
        {
            if (hit.transform.CompareTag("PickableObject") || hit.transform.CompareTag("Letter"))
            {
                pickableObject = hit.transform.gameObject;
                break;
            }
        }
        if (pickableObject != null)
        {
            currentObject = pickableObject;
            HandleRigidBody(currentObject);
            PickObject(currentObject);

            if (currentObject.TryGetComponent<CutSceneController>(out var cutsceneTrigger))
            {
                Debug.Log("Try to pick up");
                cutsceneTrigger.OnPickUp();
            }
        }
    }

    void PickObject(GameObject obj)
    {
        originalLayer = obj.layer;
        SetLayerRecursively(obj, LayerMask.NameToLayer("HeldObjects"));

        var rb = obj.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = true;
            rb.detectCollisions = false;
        }

        var collider = obj.GetComponent<Collider>();
        if (collider != null) collider.enabled = false;

        obj.transform.parent = transform; 
        if (obj.CompareTag("Letter"))
        {
            obj.transform.localPosition = new Vector3(0.25f, 0f, 0.471f); 
            obj.transform.localRotation = Quaternion.Euler(0, 270f, 90f); 
        }
        else
        {
            obj.transform.localPosition = Vector3.zero;
            obj.transform.localEulerAngles = Vector3.zero;
        }
        Debug.Log($"You picked up {obj.name}");
    }

    void SetLayerRecursively(GameObject obj, int newLayer)
    {
        if (obj == null) return;
        obj.layer = newLayer;

        foreach (Transform child in obj.transform)
        {
            if (child != null)
            {
                SetLayerRecursively(child.gameObject, newLayer);
            }
        }
    }

    void Drop()
    {
        if (currentObject != null)
        {
            var rb = currentObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = false;
                rb.detectCollisions = true;
            }

            var collider = currentObject.GetComponent<Collider>();
            if (collider != null) collider.enabled = true;

            SetLayerRecursively(currentObject, originalLayer);
            currentObject.transform.parent = null;

            Debug.Log($"You dropped {currentObject.name}");
        }
        currentObject = null;
    }
        

    private void HandleRigidBody(GameObject obj)
    {
        if (obj.GetComponent<Rigidbody>() != null)
        {
            // Make obj use physics
            Rigidbody rb = obj.GetComponent<Rigidbody>();
            if (rb.isKinematic == true)
                rb.isKinematic = false;
        }
    }
}
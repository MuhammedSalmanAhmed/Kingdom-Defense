using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Rotateable : MonoBehaviour
{
    [SerializeField] private InputAction pressed, axis;
    [SerializeField] private float speed = 1;

    private Transform cam;
    private bool rotateAllowed;
    private Vector2 rotation;
    private Quaternion initialRotation;
    private DrawProjection drawProjection;
    private CannonController cannonController;

    private void Awake()
    {
        cam = Camera.main.transform;
        initialRotation = transform.rotation;
        drawProjection = FindObjectOfType<DrawProjection>();
        cannonController = GetComponent<CannonController>();

        pressed.Enable();
        axis.Enable();
        pressed.performed += _ => { StartDragging(); };
        pressed.canceled += _ => { StopDraggingAndShoot(); };
        axis.performed += context => { rotation = context.ReadValue<Vector2>(); };

        // Register to the scene change event
        SceneManager.activeSceneChanged += OnSceneChanged;
    }

    private void StartDragging()
    {
        if (this == null) return; // Prevent any further actions if the object is destroyed
        rotateAllowed = true;
        StartCoroutine(Rotate());
        drawProjection?.ShowLine();  // Show the projection line when dragging starts
    }

    private void StopDraggingAndShoot()
    {
        if (this == null) return; // Prevent any further actions if the object is destroyed
        rotateAllowed = false;
        drawProjection?.HideLine();  // Hide the projection line when dragging stops
        cannonController?.Shoot();   // Shoot the ball
        StartCoroutine(ResetRotationAfterDelay(0.1f));  // Reset the rotation after a delay
    }

    private IEnumerator Rotate()
    {
        while (rotateAllowed)
        {
            if (this == null) yield break; // Stop coroutine if the object is destroyed
            // Apply rotation
            rotation *= speed;
            transform.Rotate(-Vector3.up, rotation.x, Space.World);
            transform.Rotate(cam.right, rotation.y, Space.World);
            yield return null;
        }
    }

    private IEnumerator ResetRotationAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        if (this != null) // Check if the object still exists
        {
            transform.rotation = initialRotation;
        }
    }

    private void OnSceneChanged(Scene oldScene, Scene newScene)
    {
        // Clean up or stop any ongoing operations
        StopAllCoroutines();
        rotateAllowed = false;
        drawProjection?.HideLine(); // Ensure the line is hidden
    }

    private void OnDestroy()
    {
        // Unregister from the scene change event to avoid memory leaks
        SceneManager.activeSceneChanged -= OnSceneChanged;
    }
}

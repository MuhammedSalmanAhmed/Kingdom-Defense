using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

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
    }

    private void StartDragging()
    {
        rotateAllowed = true;
        StartCoroutine(Rotate());
        drawProjection.ShowLine();  // Show the projection line when dragging starts
    }

    private void StopDraggingAndShoot()
    {
        rotateAllowed = false;
        drawProjection.HideLine();  // Hide the projection line when dragging stops
        cannonController.Shoot();   // Shoot the ball
        StartCoroutine(ResetRotationAfterDelay(0.1f));  // Reset the rotation after a delay
    }

    private IEnumerator Rotate()
    {
        while (rotateAllowed)
        {
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
        transform.rotation = initialRotation;
    }
}

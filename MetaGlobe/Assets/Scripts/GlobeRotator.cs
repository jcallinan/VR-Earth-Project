// GlobeRotator.cs
using UnityEngine;

public class GlobeRotator : MonoBehaviour
{
    [Tooltip("Initial speed for keyboard rotation control (for testing).")]
    public float rotationSpeed = 20f;
    private Quaternion initialRotation;

    void Start()
    {
        // Initializes state, fulfilling the Start() requirement.
        initialRotation = transform.rotation;
        Debug.Log("GlobeRotator initialized. Initial rotation stored.");
    }

    void Update()
    {
        // Executes continuous logic every frame, fulfilling the Update() requirement.
        float horizontalInput = Input.GetAxis("Horizontal");
        
        // Rotates the object continuously based on input and time.
        transform.Rotate(Vector3.up, horizontalInput * rotationSpeed * Time.deltaTime, Space.World);
    }
}

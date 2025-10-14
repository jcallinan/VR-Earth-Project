// PhysicsGrabber.cs
using UnityEngine;

public class PhysicsGrabber : MonoBehaviour
{
    public Rigidbody targetRigidbody;
    public float throwTorque = 50f;

    void Update()
    {
        // Prototype for a "knock-over" action using physics.
        if (Input.GetKeyDown(KeyCode.Space) && targetRigidbody != null)
        {
            // Applies a strong, instantaneous angular force (torque) to simulate throwing the globe.
            Vector3 randomTorque = new Vector3(
                Random.Range(-1f, 1f), 
                Random.Range(-1f, 1f), 
                Random.Range(-1f, 1f)
            ).normalized * throwTorque;

            targetRigidbody.AddTorque(randomTorque, ForceMode.Impulse);
            Debug.Log("Simulated throw! Applied impulse torque to Rigidbody.");
        }
    }
}

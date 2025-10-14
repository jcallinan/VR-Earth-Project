// POI_Interaction.cs
using UnityEngine;

public class POI_Interaction : MonoBehaviour
{
    private Vector3 defaultScale;
    public float pulseMagnitude = 0.05f;
    public float pulseSpeed = 5f;

    void Start()
    {
        // Initializes the default scale.
        defaultScale = transform.localScale;
    }

    void Update()
    {
        // Creates a continuous pulsing visual effect.
        float pulse = 1f + Mathf.Sin(Time.time * pulseSpeed) * pulseMagnitude;
        transform.localScale = defaultScale * pulse;
    }
}

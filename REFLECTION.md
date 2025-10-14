# 💡 Physics and Scene Prototype Reflection

This document fulfills the reflection requirement on prototyping a **"knock-over"** mini-game and utilizing physics components.

### 1. Physics Component Usage
* **Rigidbody:** Attached to the `Globe` to bring it under the control of the physics engine. This allows the globe to be manipulated by forces, not just its transform.
* **Collider:** The **Sphere Collider** is necessary for two reasons: (1) to define the physical boundaries for the Rigidbody's interactions, and (2) to be detected by the **Meta XR `OVRGrabber`** component.

### 2. The "Knock-over" Adaptation
The concept was adapted to simulate the rotational force of "flinging" the globe, a core feature of Google Earth VR.

* **Prototype Script:** `PhysicsGrabber.cs`
* **Core Action:** On keypress, the script calls `Rigidbody.AddTorque(..., ForceMode.Impulse);`.
* **Result:** This applies a strong, instantaneous rotational force (**Torque**) to the globe. This successfully validates that the globe's physics setup can handle the powerful rotational inputs required for a dynamic VR interaction.

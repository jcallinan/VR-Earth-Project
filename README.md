# 👓 VR Foundations: Meta XR Setup Guide

This guide details the steps necessary to integrate the **Meta XR All-in-One SDK** and enable the core grab mechanic for the MetaGlobe VR project.

## Step 1: Project Preparation (Prerequisites)

1.  **Unity Editor:** Create a new **3D (URP)** or **3D Core** project.
2.  **Install SDK:** Download the **Meta XR All-in-One SDK** from the Unity Asset Store and **Import** it via the Package Manager (My Assets).
3.  **Configure XR:**
    * Go to **Edit > Project Settings > Meta XR**. Under the checklist, pick 'Apply All'.
    * Go to **Edit > Project Settings > XR Plug-in Management**. Select 'Install XR Plugin Management' first if that is an option. Then, pick '**OpenXR**'
    
    * Follow any setup prompts from the Meta configuration tools.

    * Note:, later for Quest devices - under **Android** tab, check the **Oculus** box.

## Step 2: Building Blocks

1.  **Remove Default Camera:** Delete the scene's default `Main Camera`.
2.  Select **Meta**, **About Meta SDK**, and then **Building Blocks**

## Step 3: Enabling VR Interaction

1. Select **Camera Rig**, drag and drop into your scene.
2. Add **Interactions Rig** and **Grab Interaction**


# 🌍 MetaGlobe VR: Core Project Report

This report documents the scene structure, use of GameObjects, and fundamental C# scripting concepts.

## 1. GameObjects & Components

The **MetaGlobe VR** scene is built around a central `WorldController` GameObject to manage global logic.

### Scene Structure and Prefabs
The scene utilizes a clear parent-child structure and relies on a Prefab for reusable points of interest (POIs).

1.  **`WorldController` (Empty GameObject):** Acts as the root for all logical scene elements.
2.  **`Globe` (3D Sphere):** The central interactive element. It is a child of `WorldController`.
3.  **`POI_Marker` (Prefab):** Used for at least **five interactive instances** positioned on the globe (e.g., Paris, Giza). This demonstrates the efficient use of prefabs and GameObjects.

**Key Components on the `Globe` GameObject:**

| Component | Concept Demonstrated | Purpose |
| :--- | :--- | :--- |
| **Transform** | Position, Rotation, Scale | Defines the globe's spatial properties. |
| **Sphere Collider** | Collider component | Defines the physical boundary for collision and VR interaction. |
| **Rigidbody** | Physics component | (Added for physics simulation) Enables the globe to be physically manipulated via forces/torque. |
| **GlobeRotator (Script)** | Custom Component | Contains C# logic for rotation. |

[Screenshot of a Unity Inspector panel showing a Sphere Collider, Mesh Renderer, and the GlobeRotator script attached to a GameObject named 'Globe']

---

## 2. C# Basics: Start() and Update()

The `GlobeRotator.cs` and `POI_Interaction.cs` scripts demonstrate the core functions of a Unity script's lifecycle.

### Start() Usage
The `Start()` method is executed exactly once when the script instance is first enabled.

* **Demonstration:** In `GlobeRotator.cs`, `Start()` is used to **initialize** the `initialRotation` variable. In `POI_Interaction.cs`, it sets the **default scale** of the marker. This ensures variables are properly set before any game logic begins.

### Update() Usage
The `Update()` method is called once per frame. This is the primary loop for continuous, non-physics-dependent logic.

* **Demonstration:** In `GlobeRotator.cs`, `Update()` handles **continuous rotation** based on input. In `POI_Interaction.cs`, `Update()` drives the **pulsing visual effect** by using `Time.time` to ensure smooth, continuous changes over time.

# 💡 Physics and Scene Prototype Reflection


### 1. 3D Scene Setup and Physics Components

The core of the interactive scene relies on correct **Physics** configuration:

* **Rigidbody Component:** Added to the `Globe` GameObject. This is crucial as it allows external forces (like those from a VR grab/throw) to affect its movement. The **Is Kinematic** property is initially checked, allowing the object to be moved by the player's controllers (code) while still registering for physics events.
* **Collider Component (Sphere Collider):** Defines the physical shape for collision and interaction. Without this, the `Rigidbody` would fall through surfaces, and the Meta XR system would not recognize the globe as a grabbable object.

### 2. Prototyping the "Knock-over" Game

The traditional "knock-over" game was adapted to the **"Grab-and-Throw"** mechanic required for a Google Earth VR clone.

* **The Action:** The `PhysicsGrabber.cs` script simulates a powerful throw by detecting a keypress and calling the `Rigidbody.AddTorque()` method.
* **The Physics:** `AddTorque` applies an angular force (a rotational push) to the `Rigidbody`. By using `ForceMode.Impulse`, a large force is applied immediately, resulting in the globe spinning rapidly. This successfully proves that the **Rigidbody** is correctly receiving and reacting to rotational forces, validating the physics foundation for the final VR grabbing and manipulating interaction.


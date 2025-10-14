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


### Step 4 — Scene Setup (GameObjects & Components)
1. **Remove the placeholder camera.** In the Hierarchy select `Main Camera`, press the Delete key, and confirm that the Hierarchy is now empty.
2. **Add the VR rig supplied by the SDK.** Open the Unity menu **Oculus ▸ Tools ▸ OVRCameraRig**. A new `OVRCameraRig` appears in the Hierarchy containing `TrackingSpace`, `LeftHandAnchor`, `RightHandAnchor`, and `CenterEyeAnchor` children.
3. **Create a world controller.** Right-click the Hierarchy background, choose **Create Empty**, and rename the new GameObject to `WorldController`. Reset its Transform (gear icon ▸ *Reset*) so that position, rotation, and scale read `0,0,0` / `0,0,0` / `1,1,1`.
4. **Add the interactive globe.**
   - Right-click `WorldController`, choose **3D Object ▸ Sphere**, and rename the sphere to `Globe`.
   - With `Globe` selected, open the Inspector and click **Add Component ▸ Physics ▸ Sphere Collider** (already present by default—if so, leave it enabled).
   - Add a **Rigidbody** component. Check **Is Kinematic** so the globe follows scripted motion but still exposes physics properties to the grab system.
   - Click **Add Component ▸ Scripts ▸ GlobeRotator** and **Add Component ▸ Scripts ▸ PhysicsGrabber** so the sphere responds to rotation input and torque testing. In the `PhysicsGrabber` component, drag the `Globe` GameObject into the **Target Rigidbody** field.
5. **Prepare POI markers.**
   - Create a **3D Object ▸ Cylinder** in the Hierarchy root and rename it `POI_Marker`.
   - Resize it to a slim pin (e.g., scale `0.1, 0.5, 0.1`) using the Transform in the Inspector.
   - Right-click `POI_Marker`, choose **3D Object ▸ Text - TextMeshPro**, accept the TMP import prompt if shown, and rename the new text object to `POI_Label`. Set its text to a sample location name such as "Paris" and rotate it to face outward from the globe.
   - Select the parent `POI_Marker` and click **Add Component ▸ Scripts ▸ POI Interaction**. This script provides a constant pulsing animation so POIs remain visible.
6. **Create a prefab for reuse.** Drag the `POI_Marker` object from the Hierarchy into a folder such as `Assets/Prefabs`. Unity creates `POI_Marker.prefab`, which you can instantiate repeatedly.
7. **Populate the globe with locations.** Delete the original `POI_Marker` from the Hierarchy (the prefab remains in the Project window). Drag at least five copies of the prefab into the scene, make them children of `Globe`, and position each marker on the surface with unique names such as `Paris_POI`, `Tokyo_POI`, `Giza_POI`, `NewYork_POI`, and `Sydney_POI`. Use the Move and Rotate tools so each marker stands perpendicular to the globe.

### Step 5 — Enable VR Grab Interaction
1. **Mark the globe as grabbable.** With `Globe` selected, click **Add Component ▸ Oculus ▸ Interaction ▸ OVRGrabbable**. Leave **Allow Offhand Grab** checked so either controller can grab the globe.
2. **Configure the hand anchors as grabbers.**
   - Expand `OVRCameraRig/TrackingSpace` in the Hierarchy.
   - Select `LeftHandAnchor`, add an **OVRGrabber** component, and verify that the automatically created `Sphere Collider` (trigger) appears underneath.
   - Repeat for `RightHandAnchor`. Ensure the grabber radius colliders do **not** intersect the globe at rest; adjust `Grab Volumes` if necessary.
3. **Assign grab references.** For both `LeftHandAnchor` and `RightHandAnchor`, confirm that the newly added `Sphere Collider` is listed under **Grab Volumes**, and the **Parent Held Object** field remains empty (the grabber will populate this when the globe is grabbed at runtime).
4. **Test in the editor.** Enter Play Mode. Use a gamepad, keyboard, or headset controllers to:
   - Move the thumbstick/arrow keys and confirm the `GlobeRotator` script rotates the globe.
   - Press the grip buttons on a controller to grab the globe via `OVRGrabber` + `OVRGrabbable`.
   - Tap the Space bar to trigger `PhysicsGrabber` and observe the globe receiving a torque impulse (visible when `Is Kinematic` is unchecked for testing).

## 2. Script Highlights (Start & Update Usage)
The scripts included in `Assets/Scripts/` demonstrate essential Unity lifecycle methods.

### GlobeRotator.cs
* **`Start()`** caches the initial rotation for potential resets and logs the setup message.
* **`Update()`** reads horizontal input each frame and rotates the globe with `Time.deltaTime` so rotation speed stays frame-rate independent.

### POI_Interaction.cs
* **`Start()`** captures the default scale of the marker.
* **`Update()`** applies a pulsing multiplier using `Mathf.Sin`, making the POI gently expand and contract to attract attention.

### PhysicsGrabber.cs
* **`Update()`** listens for the Space key and then calls `Rigidbody.AddTorque` with a randomized direction, giving students an example of applying instantaneous physics forces.

## 3. Verifying Your Work Without Screenshots
Because this repository does not include captured images, use the Inspector readouts to verify setup:
* Selecting `WorldController` should show only a Transform component, confirming it is an empty organizer object.
* Selecting `Globe` should reveal (in order) `Transform`, `Sphere Collider`, `Rigidbody (Is Kinematic)`, `OVRGrabbable`, `GlobeRotator`, and `PhysicsGrabber` with the `Target Rigidbody` slot populated.
* Each POI marker should list `Transform`, `Mesh Filter`, `Mesh Renderer`, and `POI_Interaction`. The child `POI_Label` should contain a TextMeshPro component with the location name.

## 4. Repository Contents
All scripts (`GlobeRotator.cs`, `POI_Interaction.cs`, and `PhysicsGrabber.cs`) and documentation live under `Assets/` so classmates can review both the code and the procedural walkthrough in source control.
# 🌍 MetaGlobe VR: Project Summary

This document serves as the combined report for scene structure, GameObject usage, prefabs, and fundamental C# scripting concepts.

## 1. GameObjects, Components, and Prefabs

The scene uses a clear structure centered on the **`Globe`** object, with reusable **Prefabs** for points of interest (POIs).

### Scene Components Overview
* **`WorldController`:** An empty GameObject acting as the scene's root logic container.
* **`Globe` (3D Sphere):** The main interactive asset. It has a **Sphere Collider** and a **Rigidbody** component (with **Is Kinematic** checked initially).
* **Prefab Usage:** The **`POI_Marker`** prefab is instantiated **five or more times** across the globe, each with a `POI_Interaction.cs` script. This demonstrates effective prefab usage.

[Screenshot of the Unity Hierarchy showing WorldController, Globe, and at least 5 POI_Marker instances]
[Screenshot of the Unity Inspector for the Globe GameObject, showing the Transform, Sphere Collider, Rigidbody, and GlobeRotator script attached]

## 2. C# Basics: Start() and Update()

The core logic is demonstrated in the `GlobeRotator.cs` script.

### Start() Implementation
* **Purpose:** Initialization logic, executed once before the first frame.
* **Code Example:**
    ```csharp
    void Start()
    {
        // Stores the initial rotation of the object as a reference point.
        initialRotation = transform.rotation; 
    }
    ```

### Update() Implementation
* **Purpose:** Continuous game logic, executed once per frame.
* **Code Example:**
    ```csharp
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        // Applies rotation based on input, using Time.deltaTime for frame-rate independence.
        transform.Rotate(Vector3.up, horizontalInput * rotationSpeed * Time.deltaTime, Space.World); 
    }
    ```

## 3. GitHub Submission
All relevant code and documentation files have been pushed to this repository, including the C# scripts (`GlobeRotator.cs`, `POI_Interaction.cs`, `PhysicsGrabber.cs`).

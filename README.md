# 👓 VR Foundations: Meta XR Setup Guide

This guide details the steps necessary to integrate the **Meta XR All-in-One SDK** and enable the core grab mechanic for the MetaGlobe VR project.

## Step 1: Project Preparation (Prerequisites)

1.  **Unity Editor:** Create a new **3D (URP)** or **3D Core** project.
2.  **Install SDK:** Download the **Meta XR All-in-One SDK** from the Unity Asset Store and **Import** it via the Package Manager (My Assets).
3.  **Configure XR:**
    * Go to **Edit > Project Settings > XR Plug-in Management**.
    * Under the **Android** tab (for Quest devices), check the **Oculus** box.
    * Follow any setup prompts from the Meta configuration tools.

## Step 2: The Player Rig (Camera and Hands)

1.  **Remove Default Camera:** Delete the scene's default `Main Camera`.
2.  **Add OVRCameraRig:** In the Unity menu, go to **Oculus > Tools > OVRCameraRig** (or use the provided `OVRPlayerController` prefab). This GameObject serves as the player's head and hand tracking anchor.

## Step 3: Enabling VR Interaction

The core "grab" mechanic requires two key components from the Meta XR SDK:

### A. Make the Globe Grabbable (`OVRGrabbable`)

1.  Select the **`Globe`** GameObject.
2.  Ensure it has a **Collider** and **Rigidbody**.
3.  Add Component > Add the **`OVRGrabbable`** script. This component tags the object as being available for VR interaction.

### B. Enable Hands to Grab (`OVRGrabber`)

1.  Locate the **`LeftHandAnchor`** and **`RightHandAnchor`** GameObjects inside the `OVRCameraRig/TrackingSpace`.
2.  On **each Hand Anchor**, add Component > Add the **`OVRGrabber`** script. This script automatically detects the controller grip input and checks for any colliders with an `OVRGrabbable` component, initiating the grab interaction when the buttons are pressed.

This setup enables the player to physically grab, rotate, and manipulate the physics-enabled globe using the VR controllers, completing the VR interaction foundation.

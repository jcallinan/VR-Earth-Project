# 👓 VR Foundations: Meta XR Setup Guide

Follow these detailed steps to prepare a Unity project for the MetaGlobe VR sample. Each instruction includes the exact menu path so you can replicate the setup without screenshots.

## 1. Import and Configure the Meta XR SDK
1. **Open the Package Manager.** Navigate to **Window ▸ Package Manager**. Switch the dropdown in the upper-left corner to **My Assets**.
2. **Download & import Meta XR.** Locate **Meta XR All-in-One SDK**, click **Download** (if necessary), then click **Import**. Accept all prompted assets so the Oculus tools and prefabs are added to your project.
3. **Enable XR Plug-in Management.** Go to **Edit ▸ Project Settings… ▸ XR Plug-in Management**. If the package is not installed, click **Install XR Plug-in Management**. Once installed, select the **Android** tab and check the **Oculus** provider.
4. **Set build target (optional but recommended).** Open **File ▸ Build Settings…**, highlight **Android**, and click **Switch Platform** so all Meta XR components configure for Quest-style deployment.

## 2. Player & Hand Tracking Rig
1. **Remove the default camera.** In the Hierarchy find `Main Camera`, right-click, and choose **Delete**.
2. **Add the OVRCameraRig prefab.** From the menu choose **Oculus ▸ Tools ▸ OVRCameraRig**. Unity instantiates the rig with the correct head (`CenterEyeAnchor`) and controller anchors (`LeftHandAnchor`, `RightHandAnchor`).
3. **Verify tracking anchors.** Expand the `OVRCameraRig` in the Hierarchy. You should see `TrackingSpace` → `LeftHandAnchor`, `RightHandAnchor`, and `CenterEyeAnchor`. Their Transforms should be zeroed relative to `TrackingSpace` (Position `0,0,0`).

## 3. Controller Interaction Setup
1. **Prepare the grabbable object.** Ensure the object you want to grab (the `Globe`) has both a Collider (set to non-trigger) and a Rigidbody (with **Is Kinematic** enabled if you do not want gravity). These are required dependencies for Meta's grab system.
2. **Add OVRGrabbable to the object.** With the `Globe` selected, click **Add Component**, search for `OVRGrabbable`, and add it. Leave **Grab Points** empty; the component will fall back to the attached collider.
3. **Add OVRGrabber to each hand.** Select `LeftHandAnchor`, click **Add Component**, search for `OVRGrabber`, and add it. Unity automatically inserts a trigger collider to detect nearby grabbable objects. Repeat for `RightHandAnchor`.
4. **Configure grab volumes.** In each `OVRGrabber`, confirm that the generated `SphereCollider` appears in the **Grab Volumes** list. Adjust the radius so that the sphere lightly encompasses the controller model without intersecting the globe by default.
5. **Assign controller inputs.** Still in the `OVRGrabber` inspector, ensure **Grab Button** is set to `GripTrigger`. This matches the Quest controller grip buttons students expect to use.
6. **Test interaction quickly.** Enter Play Mode and squeeze the grip button on either controller. The `OVRGrabber` should highlight the `Globe` (if highlight materials are configured) and allow you to pick it up once within range.

## 4. Quick Troubleshooting Checklist
- **Nothing happens when gripping:** Double-check that the `Globe`'s Rigidbody is not set to `Is Kinematic = false` without any constraints; gravity may pull it away before you can grab it.
- **Controllers pass through the globe:** Ensure the `OVRGrabber`'s generated collider is marked as **Is Trigger**. This allows overlap detection without colliding physically.
- **POI markers fall off:** Confirm each marker is parented to the `Globe` so it inherits the globe's transform and rotation.

These steps mirror the configuration used in the MetaGlobe VR scene so that students can build the project confidently even without visual references.
This step-by-step guide documents the setup process for VR using the Meta XR All-in-One SDK, ensuring a repeatable process for future students.

## 1. VR Project Configuration
1.  **SDK Import:** Import the **Meta XR All-in-One SDK** from the Asset Store via the Unity Package Manager.
2.  **XR Plug-in Management:** In Project Settings, ensure the **Oculus** provider is checked under the **Android** tab.

## 2. Player and Hand Setup
1.  **Add Rig:** The **OVRCameraRig** is added to the scene to provide the player's head tracking (`CenterEyeAnchor`) and controller/hand tracking (`LeftHandAnchor`, `RightHandAnchor`).
2.  **Remove Default:** The default `Main Camera` must be deleted as the `OVRCameraRig` provides the new head camera.

## 3. Core Interaction Setup (Grab)
The ability to grab the globe relies on two key components:

### A. Making the Object Grabbable
* **Component:** **`OVRGrabbable`**
* **Location:** Attached to the **`Globe`** GameObject.
* **Dependencies:** Requires a **Collider** and **Rigidbody** to function.

### B. Enabling the Grabber
* **Component:** **`OVRGrabber`**
* **Location:** Attached to the **`LeftHandAnchor`** and **`RightHandAnchor`** GameObjects.
* **Function:** This component listens for the controller's grip input and uses raycasting/trigger volumes to search for and interact with nearby objects that have the `OVRGrabbable` component.

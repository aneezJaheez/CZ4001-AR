using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.XR.Interaction.Toolkit.AR;

public class XRPlacementInteraction : ARPlacementInteractable
{

    [SerializeField]
    public ARPlaceObjectsOnPlane arplaceobjectsonplane;

    public ARRaycastManager m_RaycastManager;

    public GameObject tempObj;

    public GameObject outline;

    RaycastHit Hits;

    protected override bool TryGetPlacementPose(TapGesture gesture, out Pose pose)
    {
        /*var screenCenter = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f));
        var rayHit = m_RaycastManager.Raycast(screenCenter, s_Hits, TrackableType.PlaneWithinPolygon);
        var touchedRay = Camera.main.ScreenPointToRay(gesture.startPosition);
        if (Physics.Raycast(touchedRay, out Hits))
        {
            //if (Hits.collider != null && Hits.transform.CompareTag("outline"))
            //{
                pose = s_Hits[0].pose;
                //pose = curpose;

                // Use hit pose and camera pose to check if hit test is from the
                // back of the plane, if it is, no need to create the anchor.
                // ReSharper disable once LocalVariableHidesMember -- hide deprecated camera property
                var camera = xrOrigin != null
                    ? xrOrigin.Camera
#pragma warning disable 618 // Calling deprecated property to help with backwards compatibility.
                    : (arSessionOrigin != null ? arSessionOrigin.camera : Camera.main);
#pragma warning restore 618
                if (camera == null)
                    return false;

                return Vector3.Dot(camera.transform.position - pose.position, pose.rotation * Vector3.up) >= 0f;
            //}
        }*/

        // Raycast against the location the player touched to search for planes.
        var touchedRay = Camera.main.ScreenPointToRay(gesture.startPosition);
        var hit = xrOrigin != null
        ? Physics.Raycast(touchedRay, out RaycastHit Hits)
        | GestureTransformationUtility.Raycast(gesture.startPosition, s_Hits, xrOrigin, TrackableType.PlaneWithinPolygon, m_FallbackLayerMask)
#pragma warning disable 618 // Calling deprecated property to help with backwards compatibility.
        : arSessionOrigin != null && Physics.Raycast(touchedRay, out Hits) 
        | GestureTransformationUtility.Raycast(gesture.startPosition, s_Hits, arSessionOrigin, TrackableType.PlaneWithinPolygon, m_FallbackLayerMask);
#pragma warning restore 618
        if (hit)
        {
            pose = s_Hits[0].pose;
            //pose = curpose;

            // Use hit pose and camera pose to check if hit test is from the
            // back of the plane, if it is, no need to create the anchor.
            // ReSharper disable once LocalVariableHidesMember -- hide deprecated camera property
            var camera = xrOrigin != null
                ? xrOrigin.Camera
#pragma warning disable 618 // Calling deprecated property to help with backwards compatibility.
                : (arSessionOrigin != null ? arSessionOrigin.camera : Camera.main);
#pragma warning restore 618
            if (camera == null)
                return false;

            return Vector3.Dot(camera.transform.position - pose.position, pose.rotation * Vector3.up) >= 0f;
        }

        pose = default;
        return false;
    }

    protected override GameObject PlaceObject(Pose pose)
    {
        var placementObject = Instantiate(m_PlacementPrefab, pose.position, outline.transform.rotation);
        placementObject.transform.localScale = outline.transform.localScale;

        // Create anchor to track reference point and set it as the parent of placementObject.
        var anchor = new GameObject("PlacementAnchor").transform;
        anchor.position = pose.position;
        anchor.rotation = outline.transform.rotation;
        placementObject.transform.parent = anchor;

        // Use Trackables object in scene to use as parent
        var trackablesParent = xrOrigin != null
            ? xrOrigin.TrackablesParent
#pragma warning disable 618 // Calling deprecated property to help with backwards compatibility.
            : (arSessionOrigin != null ? arSessionOrigin.trackablesParent : null);
#pragma warning restore 618
        if (trackablesParent != null)
            anchor.parent = trackablesParent;

        return placementObject;
    }

    protected override void OnEndManipulation(TapGesture gesture)
    {
        base.OnEndManipulation(gesture);

        if (gesture.isCanceled)
            return;

#pragma warning disable 618 // Calling deprecated property to help with backwards compatibility.
        if (xrOrigin == null && arSessionOrigin == null)
            return;
#pragma warning restore 618

        if (TryGetPlacementPose(gesture, out var pose))
        {
            tempObj = PlaceObject(pose);


            m_ObjectPlacementEventArgs.placementInteractable = this;
            m_ObjectPlacementEventArgs.placementObject = tempObj;
            OnObjectPlaced(m_ObjectPlacementEventArgs);
            arplaceobjectsonplane.handleObjPlacement();
        }
    }
}

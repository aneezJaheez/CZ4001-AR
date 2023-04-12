using Lean.Touch;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

[RequireComponent(typeof(ARRaycastManager))]

public class ARPlaceObjectsOnPlane : MonoBehaviour
{
    public GameObject GameObjectToPlace;
    public GameObject outline;
    public ResetSession resetSession;

    ARRaycastManager m_RaycastManager;
    public bool namecardDetected = false;
    public bool objectGenerated = false;
    ARRaycastManager raycastManager;
    ARPlaneManager planeManager;
    Pose hitPlanePose;
    bool allowObjectPlacement;

    static List<ARRaycastHit> s_Hits = new List<ARRaycastHit>();

    private void Start()
    {
        raycastManager = GetComponent<ARRaycastManager>();
        planeManager = GetComponent<ARPlaneManager>();
    }

    private void Awake()
    {
        m_RaycastManager = GetComponent<ARRaycastManager>();
    }

    void OnEnable()
    {
        LeanTouch.OnFingerTap += HandleFingerTap;
    }

    void OnDisable()
    {
        LeanTouch.OnFingerTap -= HandleFingerTap;
    }

    private void HandleFingerTap(LeanFinger _)
    {
        if (!allowObjectPlacement) return;

        // placing object if screen is touched
        if (GameObjectToPlace != null)
        {
            //var gameObj = Instantiate(GameObjectToPlace, hitPlanePose.position, outline.transform.rotation);
            GameObjectToPlace.transform.position = hitPlanePose.position;
            GameObjectToPlace.transform.rotation = outline.transform.rotation;
            GameObjectToPlace.transform.localScale = outline.transform.localScale;
            GameObjectToPlace.SetActive(true);
            //objectGenerated = true;
            outline.SetActive(false);
            // to ensure reset works
            //resetSession.GameObjectToPlace = GameObjectToPlace;
        }
    }

    public void activate3DModels(GameObject modeltoplace)
    {
        namecardDetected = true;
        GameObjectToPlace = modeltoplace;
    }

    public void deactivate3DModels()
    {
        namecardDetected = false;
    }

    public void object_deactivate()
    {
        objectGenerated = false;
    }


    public bool returnObjectGenerated()
    {
        return objectGenerated;
    }

    public bool namecardDetection()
    {
        return namecardDetected;
    }


    // Update is called once per frame
    void Update()
    {
        bool rayHit = false;
        try
        {
            var screenCenter = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f));
            rayHit = m_RaycastManager.Raycast(screenCenter, s_Hits, TrackableType.PlaneWithinPolygon);
        }
        catch (Exception ex)
        {
            rayHit = false;
        }

        if (rayHit && objectGenerated == false && namecardDetected == true)
        {
            hitPlanePose = s_Hits[0].pose;
            UpdateOutline();
            allowObjectPlacement = true;
        }
        else
        {
            allowObjectPlacement = false;
            outline.SetActive(false);
        }
    }

    void UpdateOutline()
    {
        // set to face camera
        var cameraForward = Camera.current.transform.forward;
        var cameraBearing = new Vector3(cameraForward.x, 0, cameraForward.z).normalized;
        hitPlanePose.rotation = Quaternion.LookRotation(-cameraBearing);

        if (outline.activeSelf)
        {
            outline.transform.position = hitPlanePose.position; // rotation is changed with gesture
        }
        else
        {
            outline.SetActive(true);
            outline.transform.SetPositionAndRotation(hitPlanePose.position, hitPlanePose.rotation);
        }
    }
}

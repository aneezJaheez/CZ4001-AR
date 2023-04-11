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
    Vector2 outlineSize;

    static List<ARRaycastHit> s_Hits = new List<ARRaycastHit>();

    private void Start()
    {
        raycastManager = GetComponent<ARRaycastManager>();
        planeManager = GetComponent<ARPlaneManager>();
        outlineSize = outline.GetComponent<MeshRenderer>().bounds.size;
    }

    private void Awake()
    {
        m_RaycastManager = GetComponent<ARRaycastManager>();
    }

    public void activate3DModels()
    {
        namecardDetected = true;
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
        var screenCenter = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f));
        var rayHit = m_RaycastManager.Raycast(screenCenter, s_Hits, TrackableType.PlaneWithinPolygon);

        if (rayHit && objectGenerated == false && namecardDetected == true)
        {
            var pose = s_Hits[0].pose;
            var plane = (ARPlane)s_Hits[0].trackable;

            // not enough space
            if (plane.size.x < outlineSize.x || plane.size.y < outlineSize.y)
            {
                outline.SetActive(false);
                return;
            }

            // set to face camera
            var cameraForward = Camera.current.transform.forward;
            var cameraBearing = new Vector3(cameraForward.x, 0, cameraForward.z).normalized;
            pose.rotation = Quaternion.LookRotation(-cameraBearing);

            outline.SetActive(true);
            outline.transform.SetPositionAndRotation(pose.position, pose.rotation);

            // placing object if screen is touched
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            {
                var gameObj = Instantiate(GameObjectToPlace, pose.position, pose.rotation);
                objectGenerated = true;
                outline.SetActive(false);

                // to ensure reset works
                resetSession.GameObjectToPlace = gameObj;

                raycastManager.enabled = false;
                planeManager.enabled = false;
            }
        }
    }
}

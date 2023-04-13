using Lean.Touch;
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
    public bool AllowObjectPlacement = false;

    ARRaycastManager m_RaycastManager;
    ARRaycastManager raycastManager;
    ARPlaneManager planeManager;
    Pose hitPlanePose;
    

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

    private void HandleFingerTap(LeanFinger finger)
    {
        if (!AllowObjectPlacement) return;

        var touchedRay = Camera.main.ScreenPointToRay(finger.ScreenPosition);

        if (Physics.Raycast(touchedRay, out RaycastHit hit))
        {
            if (hit.collider != null && hit.transform.CompareTag("outline"))
            {
                // placing object if screen is touched
                var gameObj = Instantiate(GameObjectToPlace, hitPlanePose.position, outline.transform.rotation);
                gameObj.transform.localScale = outline.transform.localScale;
                outline.SetActive(false);

                // to ensure reset works
                resetSession.placedObjects.Add(gameObj);

                // register object for gaze
                var gaze = GetComponent<Gaze>();
                var info = gameObj.GetComponent<InfoBehaviour>();
                if (info == null) print("info is null here");
                gaze.AddInfo(info);

                // remove until image tapped again
                AllowObjectPlacement = false;
                var modelSelector = FindObjectOfType<ModelSelector>();
                modelSelector.currentLabel.SetActive(false);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        var screenCenter = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f));
        var rayHit = m_RaycastManager.Raycast(screenCenter, s_Hits, TrackableType.PlaneWithinPolygon);

        if (rayHit && AllowObjectPlacement)
        {
            hitPlanePose = s_Hits[0].pose;
            UpdateOutline();
        }
        else
        {
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.Experimental.XR;
using UnityEngine.XR.ARSubsystems;

[RequireComponent(typeof(ARRaycastManager))]

public class ARPlaceObjectsOnPlane : MonoBehaviour
{
    public GameObject GameObjectToPlace;
    ARRaycastManager m_RaycastManager;
    public bool namecardDetected = false;
    public bool objectGenerated = false;
    ARRaycastManager arrayman;
    ARPlaneManager arplneman;

    static List<ARRaycastHit> s_Hits = new List<ARRaycastHit>();

    private void Start()
    {
        arrayman = GetComponent<ARRaycastManager>();
        arplneman = GetComponent<ARPlaneManager>();
    }

    private void Awake()
    {
        m_RaycastManager = GetComponent<ARRaycastManager>();
    }

    public void activate3DModels() {
        namecardDetected = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (m_RaycastManager.Raycast(Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0.5f)), s_Hits, TrackableType.PlaneWithinPolygon) && objectGenerated == false && namecardDetected == true) 
        {
            Pose hitPose = s_Hits[0].pose;
            GameObjectToPlace.transform.position = hitPose.position;
            objectGenerated = true;
            arrayman.enabled = false;
            arplneman.enabled = false;
            /*GameObject gallery = Instantiate(GameObjectToPlace, hitPose.position, hitPose.rotation);*/



            if (!GameObjectToPlace.active) 
            {
                GameObjectToPlace.SetActive(true);
            }
        }
    }
}

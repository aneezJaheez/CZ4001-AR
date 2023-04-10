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
    private List<GameObject> instantiatedPrefabs = new List<GameObject>();
    private List<bool> positionSetList = new List<bool>();

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

    public void addPrefabs(GameObject prefab)
    {
        instantiatedPrefabs.Add(prefab);
        positionSetList.Add(false);
    }

    // Update is called once per frame
    void Update()
    {
        int cur = 0;
        foreach (var instantiatedPrefab in instantiatedPrefabs)
        {
            bool positionSet = positionSetList[cur];
            GameObject contents = instantiatedPrefab.transform.GetChild(0).gameObject;
            GameObject model = contents.transform.GetChild(10).gameObject;
            if (model.activeSelf)
            {
                if(!positionSet && m_RaycastManager.Raycast(Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0.5f)), s_Hits, TrackableType.Planes))
                {
                    positionSetList[instantiatedPrefabs.IndexOf(instantiatedPrefab)] = true;
                    //Pose hitPose = s_Hits[0].pose;
                    Pose hitPose = s_Hits[0].sessionRelativePose;
                    model.transform.position = hitPose.position;
                    model.transform.rotation = hitPose.rotation;
                }
            }
            else
            {
                positionSetList[instantiatedPrefabs.IndexOf(instantiatedPrefab)] = false;
            }
            cur++;
        }

        /*if (m_RaycastManager.Raycast(Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0.5f)), s_Hits, TrackableType.PlaneWithinPolygon) && objectGenerated == false && namecardDetected == true) 
        {
            Pose hitPose = s_Hits[0].pose;
            GameObjectToPlace.transform.position = hitPose.position;
            objectGenerated = true;
            //arrayman.enabled = false;
            //arplneman.enabled = false;
            //GameObject gallery = Instantiate(GameObjectToPlace, hitPose.position, hitPose.rotation);



            if (!GameObjectToPlace.activeSelf) 
            {
                GameObjectToPlace.SetActive(true);
            }
        }*/
    }
}

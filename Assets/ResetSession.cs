using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class ResetSession : MonoBehaviour
{
    ARRaycastManager arrayman;
    ARPlaneManager arplneman;
    public GameObject GameObjectToPlace;
    // Start is called before the first frame update
    public void resetSession()

    {
        ARPlaceObjectsOnPlane activateModel;
        activateModel = FindObjectOfType<ARPlaceObjectsOnPlane>();
        activateModel.deactivate3DModels();
        activateModel.object_deactivate(); 
        GameObjectToPlace.SetActive(false);
        arrayman.enabled = false;
        arplneman.enabled = false;


    }
}

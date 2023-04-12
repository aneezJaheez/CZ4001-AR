using System.Collections.Generic;
using UnityEngine;

public class ResetSession : MonoBehaviour
{

    public GameObject GameObjectToPlace { get; set; }
    public List<GameObject> GameObjectsPlaced = new List<GameObject>();
    

    /*public void addPlacedObject(GameObject gameObject)
    {
        GameObjectsPlaced.Add(gameObject);
    }*/

    public void resetSession()
    {
        ARPlaceObjectsOnPlane activateModel;
        activateModel = FindObjectOfType<ARPlaceObjectsOnPlane>();
        PlaceTrackedImages trackImageScript;
        activateModel.deactivate3DModels();
        activateModel.object_deactivate();
        trackImageScript = FindObjectOfType<PlaceTrackedImages>();
        trackImageScript.resetAllModel();
        /*foreach (GameObject placedObject in GameObjectsPlaced)
        {
            placedObject.SetActive(false);
        }
        GameObjectsPlaced.Clear();*/

        //if (GameObjectToPlace && GameObjectToPlace.activeSelf) 
        //GameObjectToPlace.SetActive(false);
    }
}

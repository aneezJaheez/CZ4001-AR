using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.AR;

public class ResetSession : MonoBehaviour
{

    public List<GameObject> placedObjects;

    // Start is called before the first frame update
    public void Reset()
    {
        ARPlaceObjectsOnPlane activateModel;
        activateModel = FindObjectOfType<ARPlaceObjectsOnPlane>();
        activateModel.AllowObjectPlacement = false;

        placedObjects.ForEach(obj => { Destroy(obj); });
        placedObjects = new List<GameObject>();
    }
}

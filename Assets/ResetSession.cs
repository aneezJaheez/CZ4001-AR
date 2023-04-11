using UnityEngine;

public class ResetSession : MonoBehaviour
{

    public GameObject GameObjectToPlace { get; set; }
    // Start is called before the first frame update
    public void resetSession()
    {
        ARPlaceObjectsOnPlane activateModel;
        activateModel = FindObjectOfType<ARPlaceObjectsOnPlane>();
        activateModel.deactivate3DModels();
        activateModel.object_deactivate();

        GameObjectToPlace.SetActive(false);
    }
}

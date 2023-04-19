using TMPro;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.AR;

public class ModelSelector : MonoBehaviour
{
    public ARPlaceObjectsOnPlane objectPlacer;
    public GameObject currentLabel;
    public GameObject scanning;

    ARPlaceObjectsOnPlane placerScript;

    void Start()
    {
        placerScript = FindObjectOfType<ARPlaceObjectsOnPlane>();
        currentLabel.SetActive(false);
    }

    public void RemoveScanningUI()
    {
        scanning.SetActive(false);
    }

    public void PlaceSelectedModel(GameObject model, string label)
    {      
        objectPlacer.GameObjectToPlace = model;
        placerScript.AllowObjectPlacement = true;
        var text = currentLabel.GetComponent<TextMeshProUGUI>();
        text.text = string.Format("Placing {0}", label);
        currentLabel.SetActive(true);
    }
}

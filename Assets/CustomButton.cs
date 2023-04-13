using UnityEngine;
using UnityEngine.UI;

public class CustomButton : MonoBehaviour
{
    public GameObject model;
    public string label;

    ModelSelector modelSelector;

    void Start()
    {
        var button = GetComponent<Button>();
        button.onClick.AddListener(HandleClickEvent);
        modelSelector = FindObjectOfType<ModelSelector>();
    }

    void HandleClickEvent()
    {
        modelSelector.PlaceSelectedModel(model, label);
    }
}

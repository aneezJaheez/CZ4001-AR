using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.XR.Interaction.Toolkit.AR;

[RequireComponent(typeof(ARTrackedImageManager))]
public class PlaceTrackedImages : MonoBehaviour
{

    ARPlaceObjectsOnPlane activateModel;

    // Reference to AR tracked image manager component
    private ARTrackedImageManager _trackedImagesManager;
    ARRaycastManager arrayman;
    ARPlaneManager arplneman;
    bool object_generated, namecard_detection;

    // List of prefabs to instantiate - these should be named the same
    // as their corresponding 2D images in the reference image library 
    public GameObject[] ArPrefabs;


    private readonly Dictionary<string, GameObject> _instantiatedPrefabs = new Dictionary<string, GameObject>();
    //  foreach(var trackedImage in eventArgs.added){
    // Get the name of the reference image
    //   var imageName = trackedImage.referenceImage.name;
    //   foreach (var curPrefab in ArPrefabs) {
    // Check whether this prefab matches the tracked image name, and that
    // the prefab hasn't already been created
    //  if (string.Compare(curPrefab.name, imageName, StringComparison.OrdinalIgnoreCase) == 0
    //   && !_instantiatedPrefabs.ContainsKey(imageName)){
    // Instantiate the prefab, parenting it to the ARTrackedImage
    //  var newPrefab = Instantiate(curPrefab, trackedImage.transform);
    private void Start()
    {
        activateModel = FindObjectOfType<ARPlaceObjectsOnPlane>();
        arrayman = GetComponent<ARRaycastManager>();
        arplneman = GetComponent<ARPlaneManager>();
        arrayman.enabled = false;
        arplneman.enabled = false;
    }
    void Awake()
    {
        _trackedImagesManager = GetComponent<ARTrackedImageManager>();
    }
    void OnEnable()
    {
        _trackedImagesManager.trackedImagesChanged += OnTrackedImagesChanged;
    }
    void OnDisable()
    {
        _trackedImagesManager.trackedImagesChanged -= OnTrackedImagesChanged;
    }
    private void OnTrackedImagesChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {

        foreach (var trackedImage in eventArgs.added)
        {
            var imageName = trackedImage.referenceImage.name;
            foreach (var curPrefab in ArPrefabs)
            {
                //string.Compare(curPrefab.name, imageName, StringComparison.OrdinalIgnoreCase) == 0 && 
                if (!_instantiatedPrefabs.ContainsKey(imageName))
                {
                    var newPrefab = Instantiate(curPrefab, trackedImage.transform);
                    _instantiatedPrefabs[imageName] = newPrefab;
                    arrayman.enabled = true;
                    arplneman.enabled = true;

                    var images = FindObjectOfType<ImagesAnimation>();
                    if (images != null) images.OpenImages();

                    // remove scanning UI
                    var modelSelector = FindObjectOfType<ModelSelector>();
                    modelSelector.RemoveScanningUI();
                }

            }

        }

        foreach (var trackedImage in eventArgs.updated)
        {
            var imageName = trackedImage.referenceImage.name;
            _instantiatedPrefabs[imageName].SetActive(true);
            Transform trans = _instantiatedPrefabs[imageName].transform;
            Transform childTrans = trans.Find("Sculptures");
            Transform childTrans2 = trans.Find("Trigger_Script");
            Transform childTrans3 = trans.Find("Trigger_Button");
            GameObject sculpture, button, scriptObject;
            if (childTrans != null)
            {
                sculpture = childTrans.gameObject;
                button = childTrans3.gameObject;
                if (childTrans2 != null)
                {
                    scriptObject = childTrans2.gameObject;
                    ButtonManager script = scriptObject.GetComponent<ButtonManager>();
                    script.outOfView = !(trackedImage.trackingState == TrackingState.Tracking);
                    button.SetActive(trackedImage.trackingState == TrackingState.Tracking);
                }
            }
        }

        foreach (var trackedImage in eventArgs.removed)
        {
            // Destroy its prefab
            //Destroy(_instantiatedPrefabs[trackedImage.referenceImage.name]);
            // Also remove the instance from our array
            //_instantiatedPrefabs.Remove(trackedImage.referenceImage.name);
            // Or, simply set the prefab instance to inactive
            _instantiatedPrefabs[trackedImage.referenceImage.name].SetActive(false);

        }
    }

}
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using System.Linq;
using System.Xml.Linq;

[RequireComponent(typeof(ARTrackedImageManager))]
public class PlaceTrackedImages : MonoBehaviour
{

    ARPlaceObjectsOnPlane activateModel;

    // Reference to AR tracked image manager component
    private ARTrackedImageManager _trackedImagesManager;
    ARRaycastManager arrayman;
    ARPlaneManager arplneman;
    bool object_generated, namecard_detection;
<<<<<<< Updated upstream
    List<GameObject> ArPrefabsQueue = new List<GameObject>();
=======
    ModelSelector modelSelector;
    ImagesAnimation images;
>>>>>>> Stashed changes

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
        modelSelector = FindObjectOfType<ModelSelector>();
        images = FindObjectOfType<ImagesAnimation>();
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
<<<<<<< Updated upstream
=======
        int namecardavail = 0;
>>>>>>> Stashed changes
        foreach (var trackedImage in eventArgs.added)
        {
            var imageName = trackedImage.referenceImage.name;
            foreach (var curPrefab in ArPrefabs)
            {

                if (string.Compare(curPrefab.name, imageName, StringComparison.OrdinalIgnoreCase) == 0 && !_instantiatedPrefabs.ContainsKey(imageName))
                {
                    var newPrefab = Instantiate(curPrefab, trackedImage.transform);
                    _instantiatedPrefabs[imageName] = newPrefab;
<<<<<<< Updated upstream
                    //arrayman.enabled = true;
                    //arplneman.enabled = true;
                    //activateModel.activate3DModels();
=======
                    arrayman.enabled = true;
                    arplneman.enabled = true;

                    // remove scanning UI
                    modelSelector.RemoveScanningUI();
>>>>>>> Stashed changes
                }

            }

        }

        

        foreach (var trackedImage in eventArgs.updated)
        {
            var imageName = trackedImage.referenceImage.name;
            //_instantiatedPrefabs[imageName].SetActive(trackedImage.trackingState == TrackingState.Tracking);
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
                    script.outofview = !(trackedImage.trackingState == TrackingState.Tracking);
                    button.SetActive(trackedImage.trackingState == TrackingState.Tracking);
                    if (!sculpture.activeSelf && !ArPrefabsQueue.Contains(sculpture) && script.check_model_activation())
                        ArPrefabsQueue.Add(sculpture);
                    else if (ArPrefabsQueue.Contains(sculpture) && !script.check_model_activation())
                        ArPrefabsQueue.Remove(sculpture);
                }
            }
<<<<<<< Updated upstream
            /*if (_instantiatedPrefabs.ContainsKey(imageName))
            {
                // if namecard reappears...
                if (_instantiatedPrefabs[trackedImage.referenceImage.name].activeSelf == true) 
                {
                    activateModel.activate3DModels();

                }
                object_generated = activateModel.returnObjectGenerated();
                namecard_detection = activateModel.namecardDetection();

                if (object_generated == false && namecard_detection == true)
                {
                    arrayman.enabled = true;
                    arplneman.enabled = true;
                    activateModel.activate3DModels();
                }

            }*/

=======
            namecardavail += (trackedImage.trackingState == TrackingState.Tracking ? 0 : 1);
>>>>>>> Stashed changes
        }

        foreach (var trackedImage in eventArgs.removed)
        {
            // Destroy its prefab
            //Destroy(_instantiatedPrefabs[trackedImage.referenceImage.name]);
            // Also remove the instance from our array
            //_instantiatedPrefabs.Remove(trackedImage.referenceImage.name);
            // Or, simply set the prefab instance to inactive
<<<<<<< Updated upstream
            //_instantiatedPrefabs[trackedImage.referenceImage.name].SetActive(false);
            var imageName = trackedImage.referenceImage.name;
            //_instantiatedPrefabs[imageName].SetActive(true);
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
                    button.SetActive(false);
                    ButtonManager script = scriptObject.GetComponent<ButtonManager>();
                    script.outofview = true;
                    if (ArPrefabsQueue.Contains(sculpture))
                        ArPrefabsQueue.Remove(sculpture);
                }
            }
        }
        //Debug.Log(ArPrefabsQueue.Count);
        if (ArPrefabsQueue.Count > 0)
        {
            arrayman.enabled = true;
            arplneman.enabled = true;
            foreach (GameObject modeltoplace in ArPrefabsQueue)
            {
                if (!modeltoplace.activeSelf)
                {
                    activateModel.activate3DModels(modeltoplace);
                    break;
                } 
                else
                    ArPrefabsQueue.Remove(modeltoplace);
            }
            
        }
        else
        {
            arrayman.enabled = false;
            arplneman.enabled = false;
            activateModel.deactivate3DModels();
=======
            _instantiatedPrefabs[trackedImage.referenceImage.name].SetActive(false);
            namecardavail++;
        }

        if (namecardavail > 0)
        {
            modelSelector.StartScanningUI();
            images.CloseImages();
        }
        else
        {
            modelSelector.RemoveScanningUI();
            images.OpenImages();
>>>>>>> Stashed changes
        }
    }

    public void resetAllModel()
    {
        foreach (KeyValuePair<string, GameObject> pair in _instantiatedPrefabs)
        {
            GameObject ARPrefabs = pair.Value;
            Transform trans = ARPrefabs.transform;
            Transform childTrans = trans.Find("Trigger_Script");
            GameObject scriptObject;

            if (childTrans != null)
            {
                scriptObject = childTrans.gameObject;
                ButtonManager script = scriptObject.GetComponent<ButtonManager>();
                script.disable_model();
            }
        }
    }
}
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class Gaze : MonoBehaviour
{
    List<InfoBehaviour> infos = new List<InfoBehaviour>();
    Transform cameraTransform;

    // Start is called before the first frame update
    void Start()
    {
        infos = FindObjectsOfType<InfoBehaviour>(true).ToList();
        cameraTransform = Camera.main.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (Physics.Raycast(cameraTransform.position, cameraTransform.forward, out RaycastHit hit))
        {
            GameObject go = hit.collider.gameObject;
            if (go.CompareTag("hasInfo"))
            {
                OpenInfo(go.GetComponent<InfoBehaviour>());
            }
            else
            {
                CloseAll();
            }
        }

    }

    void OpenInfo(InfoBehaviour desiredInfo)
    {
        foreach (InfoBehaviour info in infos)
        {
            if (info == desiredInfo)
            {
                info.OpenInfo();
            }
            else
            {
                info.CloseInfo();
            }

        }
    }

    void CloseAll()
    {
        foreach (InfoBehaviour info in infos)
        {
            info.CloseInfo();
        }
    }
}

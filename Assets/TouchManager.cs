using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using TMPro;

public class TouchManager : MonoBehaviour
{
    public GameObject panelVideo, panelArtwork;
    public GameObject scriptObject;
    ButtonManager script;
    //public TextMeshProUGUI text_change;
    // Start is called before the first frame update
    void Start()
    {
        panelVideo.SetActive(false);
        panelArtwork.SetActive(false);
        script = scriptObject.GetComponent<ButtonManager>();
    }

    // Update is called once per frame
    void Update()
    {
   
        if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began) 
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.touches[0].position);
            RaycastHit hit;
            
            if (Physics.Raycast(ray, out hit)) 
            {
                if(hit.collider != null) 
                {
                    //Color newColor = new Color(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), 1.0f);
                    //hit.collider.GetComponent<MeshRenderer>().material.color = newColor;

                    if (hit.collider.tag == "ArtworkVideo")
                    {
                        if (panelVideo.activeSelf == false)
                        {
                            panelVideo.SetActive(true);
                            if (panelArtwork.activeSelf == true)
                                panelArtwork.SetActive(false);
                        }
                        else panelVideo.SetActive(false);


                    }

                    else if (hit.collider.tag == "ArtworkPainting")
                    {
                        if (panelArtwork.activeSelf == false)
                        {
                            panelArtwork.SetActive(true);

                            if (panelVideo.activeSelf == true)
                                panelVideo.SetActive(false);
                        }

                        else panelArtwork.SetActive(false);
                    }

                    else if (hit.collider.tag == "Model")
                    {
                        panelArtwork.SetActive(false);
                        panelVideo.SetActive(false);
                        script.toggle_model();
                    }


                    else if (hit.collider.tag == "WebsiteURL")
                    {

                        if (panelVideo.activeSelf == true || panelArtwork.activeSelf == true)
                            panelVideo.SetActive(false);
                            panelArtwork.SetActive(false);

                        Application.OpenURL("https://www.nationalgallery.sg/");
                    }

                    else if (hit.collider.tag == "LinkedIn")
                    {
                        if (panelVideo.activeSelf == true || panelArtwork.activeSelf == true)
                            panelVideo.SetActive(false);
                            panelArtwork.SetActive(false);
                    }

                }
            }
        }

    }
}

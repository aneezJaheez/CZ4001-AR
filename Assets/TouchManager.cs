using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TouchManager : MonoBehaviour
{
    public GameObject panel, model;
    // Start is called before the first frame update
    void Start()
    {
        panel.SetActive(false);
        model.SetActive(false);
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

                    if (hit.collider.tag == "Artwork")
                    {
                        if (panel.activeSelf == false)
                            panel.SetActive(true);
                        else panel.SetActive(false);


                    }
                    else if (hit.collider.tag == "Model") 
                    {
                        if (model.activeSelf == false)
                            model.SetActive(true);
                        else model.SetActive(false);


                    }


                    else if (hit.collider.tag == "WebsiteURL")
                    {

                        if (panel.activeSelf == true)
                            panel.SetActive(false);

                        Application.OpenURL("https://www.nationalgallery.sg/");
                    }

                    else if (hit.collider.tag == "LinkedIn")
                    {
                        if (panel.activeSelf == true)
                            panel.SetActive(false);
                    }

                }
            }
        }

    }
}

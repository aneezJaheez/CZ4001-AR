using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using TMPro;

public class TouchManager : MonoBehaviour
{
    public GameObject panelPort, panelShoe, panelStarry, panelSkull, panelField;
    public GameObject scriptObject;
    ButtonManager script;
    //public TextMeshProUGUI text_change;
    // Start is called before the first frame update
    void Start()
    {
        setAllInactive();
        script = scriptObject.GetComponent<ButtonManager>();
    }

    void setAllInactive() 
    {
        panelPort.SetActive(false);
        panelShoe.SetActive(false);
        panelStarry.SetActive(false);
        panelSkull.SetActive(false);
        panelField.SetActive(false);

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
                if (hit.collider != null)
                {
                    //Color newColor = new Color(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), 1.0f);
                    //hit.collider.GetComponent<MeshRenderer>().material.color = newColor;

                    if (hit.collider.tag == "paintingShoe")
                    {
                        if (panelShoe.activeSelf == false)
                        {
                            setAllInactive();
                            panelShoe.SetActive(true);
                        }
                        else panelShoe.SetActive(false);


                    }

                    else if (hit.collider.tag == "paintingPort")
                    {
                        if (panelPort.activeSelf == false)
                        {

                            setAllInactive();
                            panelPort.SetActive(true);

                        }
                        else panelPort.SetActive(false);
                    }

                    else if (hit.collider.tag == "paintingSkull")
                    {
                        if (panelSkull.activeSelf == false)
                        {

                            setAllInactive();
                            panelSkull.SetActive(true);

                        }
                        else panelSkull.SetActive(false);
                    }


                    else if (hit.collider.tag == "paintingField")
                    {
                        if (panelField.activeSelf == false)
                        {

                            setAllInactive();
                            panelField.SetActive(true);

                        }
                        else panelField.SetActive(false);
                    }


                    else if (hit.collider.tag == "paintingStarry")
                    {
                        if (panelStarry.activeSelf == false)
                        {

                            setAllInactive();
                            panelStarry.SetActive(true);

                        }
                        else panelStarry.SetActive(false);
                    }


                    else if (hit.collider.tag == "Model")
                    {
                        setAllInactive();
                        script.toggle_model();
                    }


                    else if (hit.collider.tag == "WebsiteURL")
                    {

                        setAllInactive();
                        Application.OpenURL("https://www.vangoghmuseum.nl/en");
                    }
                    else
                      setAllInactive();

                }

                
            }
        }

    }
}

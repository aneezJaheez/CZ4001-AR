using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    public GameObject contents, cardanimation, model;
    public bool outofview = true;
    bool activate_model = false;
    bool activate_contents = false;

    // Start is called before the first frame update
    void Start()
    {
        contents.SetActive(false);
        //cardanimation.SetActive(false);
        model.SetActive(false);
    }

    public void toggle_model()
    {
        activate_model = !activate_model;
        if (model.activeSelf && !activate_model)
            model.SetActive(false);
    }

    public bool check_model_activation()
    {
        return activate_model;
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
                if (hit.collider != null && hit.collider.tag == "Trigger")
                {
                    activate_contents = !activate_contents;
                }
            }
        }
        if (activate_contents)
        {
            //if (!cardanimation.activeSelf)
                //cardanimation.SetActive(true);
            if (!outofview && !contents.activeSelf)
            {
                contents.SetActive(true);
            }
            else if (outofview)
            {
                contents.SetActive(false);
            }
        }
        else
        {
            contents.SetActive(false);
            //cardanimation.SetActive(false);
        }
    }
}

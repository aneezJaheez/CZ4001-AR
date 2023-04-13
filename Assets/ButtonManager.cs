using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    public GameObject contents, cardAnimation;
    public bool outOfView = true;

    bool activate_contents = false;

    // Start is called before the first frame update
    void Start()
    {
        contents.SetActive(false);
        cardAnimation.SetActive(false);
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
            if (!cardAnimation.activeSelf)
                cardAnimation.SetActive(true);
            if (!outOfView && !contents.activeSelf)
            {
                contents.SetActive(true);
            }
            else if (outOfView)
            {
                contents.SetActive(false);
            }
        }
        else
        {
            contents.SetActive(false);
            cardAnimation.SetActive(false);
        }
    }
}

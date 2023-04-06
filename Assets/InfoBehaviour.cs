using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    Transform SectionInfo;
    Vector3 desiredScale = Vector3.zero;
    const float SPEED = 6f;


    // Update is called once per frame
    void Update()
    {
        SectionInfo.localScale = Vector3.Lerp(SectionInfo.localScale, desiredScale, Time.deltaTime*SPEED);
        
    }

    public void OpenInfo() 
    {
        desiredScale = Vector3.one;
    }

    public void CloseInfo()
    {
        desiredScale = Vector3.zero;
    }
}

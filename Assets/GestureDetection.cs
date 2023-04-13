using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestureDetection : MonoBehaviour
{
    public GameObject Outline;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Outline.activeSelf)
        {
            var pinchScale = Lean.Touch.LeanGesture.GetPinchScale(); // 0 - 1 scale
            Outline.transform.localScale *= pinchScale;

            var twistDeg = Lean.Touch.LeanGesture.GetTwistDegrees(); //  - right, + left
            Outline.transform.Rotate(0f, -twistDeg, 0f);
        }
    }
}

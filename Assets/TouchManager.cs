using UnityEngine;
using UnityEngine.XR.ARFoundation;
using Lean.Touch;

public class TouchManager : MonoBehaviour
{
    public GameObject panelPort, panelShoe, panelStarry, panelSkull, panelField;

    // Start is called before the first frame update
    void Start()
    {
        SetAllInactive();
    }

    void OnEnable()
    {
        LeanTouch.OnFingerTap += CardTouchHandler;
    }

    void OnDisable()
    {
        LeanTouch.OnFingerTap -= CardTouchHandler;
    }

    void CardTouchHandler(LeanFinger finger)
    {
        var touchedRay = Camera.main.ScreenPointToRay(finger.ScreenPosition);
        if (Physics.Raycast(touchedRay, out RaycastHit hit))
        {
            if (hit.collider == null) return;
            SetAllInactive();

            switch (hit.collider.tag)
            {
                case "paintingShoe":
                    panelShoe.SetActive(true);
                    break;
                case "paintingPort":
                    panelPort.SetActive(true);
                    break;
                case "paintingSkull":
                    panelSkull.SetActive(true);
                    break;
                case "paintingField":
                    panelField.SetActive(true);
                    break;
                case "paintingStarry":
                    panelStarry.SetActive(true);
                    break;
                case "WebsiteURL":
                    Application.OpenURL("https://www.nationalgallery.sg/");
                    break;
                default: break;
            }
        }
    }

    void SetAllInactive()
    {
        panelPort.SetActive(false);
        panelShoe.SetActive(false);
        panelStarry.SetActive(false);
        panelSkull.SetActive(false);
        panelField.SetActive(false);
    }
}

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
                    if (!panelShoe.activeSelf)
                        panelShoe.SetActive(true);
                    else
                        panelShoe.SetActive(false);
                    break;
                case "paintingPort":
                    if (!panelPort.activeSelf)
                        panelPort.SetActive(true);
                    else
                        panelPort.SetActive(false);
                    break;
                case "paintingSkull":
                    if (!panelSkull.activeSelf)
                        panelSkull.SetActive(true);
                    else
                        panelSkull.SetActive(false);
                    break;
                case "paintingField":
                    if (!panelField.activeSelf)
                        panelField.SetActive(true);
                    else
                        panelField.SetActive(false);
                    break;
                case "paintingStarry":
                    if (!panelStarry.activeSelf)
                        panelStarry.SetActive(true);
                    else
                        panelStarry.SetActive(false);
                    break;
                case "WebsiteURL":
                    Application.OpenURL("https://www.vangoghmuseum.nl/en");
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

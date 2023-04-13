using UnityEngine;

public class ImagesAnimation : MonoBehaviour
{
    // Script is a copy of InfoBehaviour for now

    float desiredScale = 0f;
    const float SPEED = 6f;

    void Update()
    {
        transform.localScale = new Vector3(1f, Mathf.Lerp(transform.localScale.y, desiredScale, Time.deltaTime * SPEED), 1f);
    }

    public void OpenImages()
    {
        desiredScale = 1f;
    }

    public void CloseImages()
    {
        desiredScale = 0f;
    }
}

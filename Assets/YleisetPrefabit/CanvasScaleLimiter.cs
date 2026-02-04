using UnityEngine;
using UnityEngine.UI;

public class CanvasScaleLimiter : MonoBehaviour
{
    public CanvasScaler canvasScaler;
    public float maxAspectRatio = 16f / 9f;  // The target aspect ratio you want to limit to (16:9)

    void Start()
    {
        if (canvasScaler == null)
        {
            canvasScaler = GetComponent<CanvasScaler>();
        }

        LimitCanvasScale();
    }

    void Update()
    {
        LimitCanvasScale();
    }

    void LimitCanvasScale()
    {
        float currentAspectRatio = (float)Screen.width / Screen.height;

        // Check if the aspect ratio exceeds the defined max aspect ratio (e.g., 16:9)
        if (currentAspectRatio > maxAspectRatio)
        {
            float limitedWidth = maxAspectRatio * Screen.height;
            canvasScaler.referenceResolution = new Vector2(limitedWidth, Screen.height);
        }
        else
        {
            // Restore normal scaling if aspect ratio is within bounds
            canvasScaler.referenceResolution = new Vector2(Screen.width, Screen.height);
        }
    }
}

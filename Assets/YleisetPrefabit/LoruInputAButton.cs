
using UnityEngine;

using UnityEngine.EventSystems;

public class LoruInputAButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public bool pressed;

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("Button pressed!");
        pressed = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Debug.Log("Button released!");
        pressed = false;
    }
    public bool released
    {
        get
        {
            bool wasPressed = pressed;
            pressed = false; // Reset pressed state after checking
            return wasPressed;
        }
    }
}

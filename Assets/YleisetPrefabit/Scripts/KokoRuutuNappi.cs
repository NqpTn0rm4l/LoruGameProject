using UnityEngine;
using UnityEngine.EventSystems;

public class KokoRuutuNappi : MonoBehaviour, IPointerDownHandler
{
    public void OnPointerDown(PointerEventData eventData)
    {
  
        if (!Screen.fullScreen)
        {
            Screen.fullScreen = true;

        }
        else
        {
            Screen.fullScreen = false;

        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasCamera : MonoBehaviour
{
    // Start is called before the first frame update
    private void Awake()
    {
        GetComponent<Canvas>().worldCamera = Camera.main;
    }
    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIBlur : MonoBehaviour
{
    public Camera blurCam;
    public Material blurMat;

    // Start is called before the first frame update
    void Start()
    {
        if (blurCam.targetTexture != null)
        {
            blurCam.targetTexture.Release();
        }
       
        blurMat.SetTexture("camText", blurCam.targetTexture);
        print(blurCam.targetTexture);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

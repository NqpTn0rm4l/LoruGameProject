using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KameranKaantaja : MonoBehaviour
{
    Camera kamera;

    bool vaarinPain;

    private void Start()
    {
        kamera = Camera.main;
        vaarinPain = false;
    }

    private void Update()
    {
        if (!Pelikohtaiset.onAinaLandscape) return;
        kamera = Camera.main;
        if (kamera.pixelWidth < kamera.pixelHeight)
        {
            kamera.transform.rotation = Quaternion.Euler(0, 0, 90);

            vaarinPain = true;
        }
        else
        {
            if (vaarinPain)
            {
                kamera.transform.rotation = Quaternion.Euler(0, 0, 0);

                vaarinPain = false;
            }
        }
    }
}

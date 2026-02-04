using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HiljennaAanet : MonoBehaviour
{
    Button nappi;
    float alkperVol;
    Color variAlussa;
    bool volumeMutedSave;

    private void Start()
    {
        alkperVol = AudioListener.volume;
        nappi = GetComponent<Button>();
        variAlussa = nappi.colors.normalColor;
        volumeMutedSave = PlayerPrefs.GetInt("VolumeMutedSave") != 0;
        if (volumeMutedSave == aanetPaalla)
        {
            VaihdaAaniTila();
        }
    }
    bool aanetPaalla = true;



    public void VaihdaAaniTila()
    {
        if (aanetPaalla)
        {
            AudioListener.volume = 0;
            aanetPaalla = false;
            nappi.image.color = Color.gray;
            PlayerPrefs.SetInt("VolumeMutedSave", 1);

        }
        else
        {
            AudioListener.volume = alkperVol;
            aanetPaalla = true;
            nappi.image.color = variAlussa;
            PlayerPrefs.SetInt("VolumeMutedSave", 0);
        }

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            VaihdaAaniTila();
        }
    }

}

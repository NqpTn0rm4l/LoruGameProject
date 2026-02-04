using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TakaisinNappi : MonoBehaviour
{
    public void Takaisin()
    {
        if (SceneManager.GetActiveScene().buildIndex != 0)
        {
            PeliManageri.SiirryValikkoon();

        }
        else
        {
            PeliManageri.Poistu();
        }

    }
}

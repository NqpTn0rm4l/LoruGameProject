using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ohjeanimaatio : MonoBehaviour
{
    public float nayttamisAika;
    public Animator anim;
    public Image sormiKuva;
    public bool naytanOhjeen;
    public string animaationNimi = "OhjeKasi";
    public GameObject seuraavaAnimaatio;
    float afkTimer;

    void Start()
    {
        naytanOhjeen = true;
        Invoke("NaytaOhje", nayttamisAika);
    }

    void Update()
    {
        if ( Input.anyKeyDown)
        {
            afkTimer = 0;
            naytanOhjeen = false;
            anim.StopPlayback();
            anim.enabled = false;
            sormiKuva.enabled = false;
            if (seuraavaAnimaatio != null)
            {
                seuraavaAnimaatio.SetActive(true);
                Destroy(gameObject);
            }   
        }
        afkTimer += Time.deltaTime;
        if (afkTimer >20)
        {
            naytanOhjeen = true;
        }

    }


    void NaytaOhje()
    {
        if (naytanOhjeen)
        {
            sormiKuva.enabled = true;
            anim.enabled = true;
            anim.Play(animaationNimi);
            Invoke("NaytaOhje", nayttamisAika);
        }

        
        Invoke("NaytaOhje", nayttamisAika);
    }
}

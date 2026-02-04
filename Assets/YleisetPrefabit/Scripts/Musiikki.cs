using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Musiikki : MonoBehaviour
{
    AudioSource ads;
    float volaAlussa;
    public GameObject Vaikeustaso;
   
    // Start is called before the first frame update
    void Start()
    {
        Vaikeustaso.SetActive(true);
        DontDestroyOnLoad(gameObject);
        ads = GetComponent<AudioSource>();
        if (!ads.isPlaying)
        {
            ads.Play();
        }
        volaAlussa = ads.volume;
        
    }

    public void HiljennaMusa()
    {
        ads.volume = volaAlussa* 0.4f;
    }

    public void PalautaMusa()
    {
        ads.volume = volaAlussa;
    }

    public void VaimennaMusa()
    {
       
        ads.volume = 0f;
    }


    private static Musiikki playerInstance;
    void Awake()
    {
        DontDestroyOnLoad(this);

        if (playerInstance == null)
        {
            playerInstance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Palautekysely : MonoBehaviour
{
    int moneskoKertaValikossa;
    bool kaytossa;
    GameObject teksi;
    // Start is called before the first frame update
    void Start()
    {
        teksi = transform.GetChild(0).gameObject;
        teksi.SetActive(false );
        GetComponent<SpriteRenderer>().enabled = false;
        moneskoKertaValikossa = PlayerPrefs.GetInt("MoneskoKertaValikossa");
        moneskoKertaValikossa++;
        PlayerPrefs.SetInt("MoneskoKertaValikossa", moneskoKertaValikossa);
        if (moneskoKertaValikossa > 5)
        {
            #if UNITY_WEBGL
               GetComponent<SpriteRenderer>().enabled = true;
                teksi.SetActive(true);
                kaytossa = true;
            #endif


        }
    }


    private void OnMouseDown()
    {
        if (Input.GetMouseButtonDown(0) && kaytossa)
        {
            if (SceneManager.GetActiveScene().name == "ValikkoVuohi")
            {
                Application.OpenURL("https://docs.google.com/forms/d/e/1FAIpQLSdSEg9yAxCVHelTRXBieBTlYEU-S9h1yw2qcHZYncHOhf-VUg/viewform?usp=sf_link");
            }
            else
            {
                Application.OpenURL("https://docs.google.com/forms/d/e/1FAIpQLSeIWO4wfBDeWwre2ssNjXUKzx1w5WIP6xoUIDpmrExSpBCsZg/viewform?usp=sf_link");
            }
            
            PlayerPrefs.SetInt("MoneskoKertaValikossa", 0);
            teksi = transform.GetChild(0).gameObject;
            teksi.SetActive(false);
            GetComponent<SpriteRenderer>().enabled = false;
        }
    }
}

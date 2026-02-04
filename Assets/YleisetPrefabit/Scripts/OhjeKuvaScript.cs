using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class OhjeKuvaScript : MonoBehaviour
{
    public string ohjeKysymys;
    public string vastaus;
    public string nappaimistoTeksti;
    public string nayttoTeksi;
    public string kuva1Teksti;
    public string kuva2Teksti;


    public TextMeshProUGUI ohjeKysymysObj;
    public TextMeshProUGUI[] vastausObj;
    public TextMeshProUGUI nappaimistoTekstiObj;
    public TextMeshProUGUI nayttoTekstiObj;

    public TextMeshProUGUI kuva1TekstiObj;
    public TextMeshProUGUI kuva2TekstiObj;
    // Start is called before the first frame update
    void Start()
    {
        foreach (var item in vastausObj)
        {
            item.text = vastaus;
        }
        nayttoTekstiObj.text = nayttoTeksi;
        ohjeKysymysObj.text = ohjeKysymys;
        nappaimistoTekstiObj.text = nappaimistoTeksti;
        kuva1TekstiObj.text = kuva1Teksti;
        kuva2TekstiObj.text = kuva2Teksti;
    }

    // Update is called once per frame
 
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class VastausTekstinAsettaja : MonoBehaviour
{

    TextMeshPro tm;
    TextMeshProUGUI tmGui;
    string vastausTeksti;
    public static List<TextMeshPro> kaikkiTekstit = new List<TextMeshPro>(4);
    public static List<TextMeshProUGUI> kaikkiTekstitGUI = new List<TextMeshProUGUI>(4);
    


    //0 = oikea 123 on väärät;
    public int mikaVastaus;
    private void Start()
    {
        if (GetComponentInChildren<TextMeshPro>() != null)
        {
            tm = GetComponentInChildren<TextMeshPro>();
            tm.enableAutoSizing = true;
        }
        if (GetComponentInChildren<TextMeshProUGUI>() != null)
        {
            tmGui = GetComponentInChildren<TextMeshProUGUI>();
            tmGui.enableAutoSizing = true;
        }

        
    }
    public void AsetaTeksti()
    {
        if (GetComponentInChildren<TextMeshPro>() != null)
        {
            GetComponentInChildren<TextMeshPro>().enableAutoSizing = true;
        }
        else if(GetComponentInChildren<TextMeshProUGUI>() != null)
        {
            GetComponentInChildren<TextMeshProUGUI>().enableAutoSizing = true;
        }
        else
        {
            Debug.LogWarning("En löydä vastausobjekteista TextMEshpro komponentteja!");
        }
        
        if (mikaVastaus == 0)
        {
            gameObject.name = "OVastaus";
        }
        if (mikaVastaus == 1)
        {
            gameObject.name = "VVastausYksi";
        }
        if (mikaVastaus == 2)
        {
            gameObject.name = "VVastausKaksi";
        }
        if (mikaVastaus == 3)
        {
            gameObject.name = "VVastausKolme";
        }

        // avataan seuraava tehtävä
        TehtavaLuokka th = VastausKontrolleri.tehtavaNyt;

        if (GetComponentInChildren<TextMeshPro>() == null && GetComponentInChildren<TextMeshProUGUI>() == null)
        {
            Debug.LogError("Laita vastausvaihtoehtojen child objekteihin TextMeshPro tai GUI komponentti");
        }
        if (GetComponentInChildren<TextMeshPro>() != null)
        {
       
            tm = GetComponentInChildren<TextMeshPro>();
        }

        if (GetComponentInChildren<TextMeshProUGUI>() != null)
        {
       
            tmGui = GetComponentInChildren<TextMeshProUGUI>();
        }

        

        if (GetComponentInChildren<MeshRenderer>() != null)
        {
            GetComponentInChildren<MeshRenderer>().sortingOrder = 10;
        }
        

        //asetetaan oikea teksti oikealle objektille
        if (gameObject.name == "OVastaus")
        {
            vastausTeksti = th.oVastaus;
            gameObject.tag = "OVastaus";
        }

        if (gameObject.name == "VVastausYksi")
        {
            //jos arvotaa ei ole asetettu laitetaan väärä muista oikeista
            if (th.vVastaus1 != "" && th.vVastaus1 != null)
            {
                vastausTeksti = th.vVastaus1;
            }
            else
            {
                vastausTeksti = VastausKontrolleri.vaaratVaihtoehdot[0];
            }
            gameObject.tag = "VVastaus";
        }
        if (gameObject.name == "VVastausKaksi")
        {   
            if (th.vVastaus2 != "" && th.vVastaus2 != null)
            {
                vastausTeksti = th.vVastaus2;
            }
            else
            {
                vastausTeksti = VastausKontrolleri.vaaratVaihtoehdot[1];
            }
            gameObject.tag = "VVastaus";
        }

        if (gameObject.name == "VVastausKolme")
        {
            if (th.vVastaus3 != "" && th.vVastaus3 != null)
            {
                vastausTeksti = th.vVastaus3;
            }
            else
            {
                vastausTeksti = VastausKontrolleri.vaaratVaihtoehdot[2];
            }
            gameObject.tag = "VVastaus";
        }
        if (tm != null)
        {
            kaikkiTekstit.Add(tm);
            tm.text = vastausTeksti;
        }
        if (tmGui != null)
        {
            kaikkiTekstitGUI.Add(tmGui);
            tmGui.text = vastausTeksti;
        }
           
    }
    public static void SkaalaaTeksti()
    {
        float fontMaksKoko = 40;
        if (kaikkiTekstit[0] != null)
        {
            foreach (var item in kaikkiTekstit)
            {
                if (item.fontSize < fontMaksKoko)
                {
                    fontMaksKoko = item.fontSize;
                }
            }
        }
        if (kaikkiTekstitGUI[0] != null)
        {
            foreach (var item in kaikkiTekstitGUI)
            {
                if (item.fontSize < fontMaksKoko)
                {
                    fontMaksKoko = item.fontSize;
                }
            }
        }
    }
}

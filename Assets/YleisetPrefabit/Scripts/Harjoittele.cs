using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Harjoittele : MonoBehaviour
{
    TehtavaLuokka vaikein1;
    TehtavaLuokka vaikein2;
    TehtavaLuokka vaikein3;
 

    TehtavaLuokka helpoin;
    
    public TextMeshProUGUI vaik1teksi;
    public TextMeshProUGUI vaik2teksi;
    public TextMeshProUGUI vaik3teksi;


    public TextMeshProUGUI helppoteksi;

    // Start is called before the first frame update
    void Start()
    {

        PaivitaHarjoiteltavat();
    }

    // Update is called once per frame
    void PaivitaHarjoiteltavat()
    {
        //kesken
        /*
        List<TehtavaLuokka> tmp  = new List<TehtavaLuokka>();
        
        for (int i = 0; i < Tehtavat.tehtavatTassaSessiossa.Count; i++)
        {
            tmp.Add(Tehtavat.tehtavatTassaSessiossa[i]);
        } */
       
        vaikein1 = Tehtavat.tehtavatTassaSessiossa[0];
        vaikein2 = Tehtavat.tehtavatTassaSessiossa[1];
        vaikein3 = Tehtavat.tehtavatTassaSessiossa[2];
    

        helpoin = Tehtavat.tehtavatTassaSessiossa[Tehtavat.tehtavatTassaSessiossa.Count - 1];

        //todo, tähän vain ne jotka on alle 50 tasolta, vastaukset pois
        vaik1teksi.text = vaikein1.tehtavaKysymys.ToString() + " " + vaikein1.oVastaus.ToString();
        vaik2teksi.text = vaikein2.tehtavaKysymys.ToString() + " " + vaikein2.oVastaus.ToString();
        vaik3teksi.text = vaikein3.tehtavaKysymys.ToString() + " " + vaikein3.oVastaus.ToString();
  

        helppoteksi.text = helpoin.tehtavaKysymys.ToString() + " " + helpoin.oVastaus.ToString();
        
    }
}

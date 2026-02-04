using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class AiheidenValinta : MonoBehaviour
{
    public GameObject valintaIkkuna;
    public Sprite valittu;
    public Sprite eiValittu;

    public Image yhteenlaskuNappi;
    public Image kertolaskuNappi;
    public Image jakolaskuNappi;

    public bool yhteenLaskut;
    public bool kertoLaskut;
    public bool jakoLaskut;
    // Start is called before the first frame update
    void Start()
    {
        if (string.IsNullOrEmpty(PlayerPrefs.GetString("EkaKerta")))
        {
            PlayerPrefs.SetInt("yhteenLaskut", 1);
            PlayerPrefs.SetInt("kertoLaskut", 1);
            PlayerPrefs.SetInt("jakoLaskut", 1);
            PlayerPrefs.SetString("EkaKerta", "true");
        }
        //ValintaIkkunaPois();
        yhteenLaskut = toBool(PlayerPrefs.GetInt("yhteenLaskut"));
        kertoLaskut = toBool(PlayerPrefs.GetInt("kertoLaskut"));
        jakoLaskut = toBool(PlayerPrefs.GetInt("jakoLaskut"));

        if (yhteenLaskut)
        {
            yhteenlaskuNappi.sprite = valittu;
        }
        if (kertoLaskut)
        {
            kertolaskuNappi.sprite = valittu;
        }
        if (jakoLaskut)
        {
            jakolaskuNappi.sprite = valittu;
        }
    }
    bool toBool(int numero)
    {
        bool paalla;
        if (numero == 1)
        {
            paalla = true;
        }
        else
        {
            paalla = false;
        }
        return paalla;
    }
    
    public void ValintaIkkunaPaalle()
    {
        DataManager.TallennaPelaajanData_Vanhentunut();
        //DataManager.Instance.ConnectionHandler.TryGetData();
        if (valintaIkkuna.activeSelf == true)
        {
            ValintaIkkunaPois();
        }
        else
        {

            valintaIkkuna.SetActive(true);
        }    
        
    }
    public void ValintaIkkunaPois()
    {
        valintaIkkuna.SetActive(false);
        //Tehtavat.tehtavatTassaSessiossa = Tehtavat.tehtavatTassaSessiossa
        //    .Where(t => yhteenLaskut && (t.settiID == 1 
        //    || t.settiID == 2) 
        //    || kertoLaskut && t.settiID == 3 
        //    || jakoLaskut && t.settiID == 4).ToList();     
    }

    public void YhteenlaskutValittu()
    {
        if (yhteenLaskut && (kertoLaskut||jakoLaskut))
        {
            YhteenlaskutPois();
        }
        else
        {
            PlayerPrefs.SetInt("yhteenLaskut", 1);
            yhteenLaskut = true;
            yhteenlaskuNappi.sprite = valittu;
        }
        
    }

    public void YhteenlaskutPois()
    {
        PlayerPrefs.SetInt("yhteenLaskut", 0);
        yhteenLaskut = false;
        yhteenlaskuNappi.sprite = eiValittu;

    }
    public void KertolaskutValittu()
    {
        if (kertoLaskut && (yhteenLaskut || jakoLaskut))
        {

            KertolaskutPois();
        }
        else
        {
            PlayerPrefs.SetInt("kertoLaskut", 1);
            kertoLaskut = true;
            kertolaskuNappi.sprite = valittu;
        }
       
    }
    public void KertolaskutPois()
    {
        PlayerPrefs.SetInt("kertoLaskut", 0);
        kertoLaskut = false;
       kertolaskuNappi.sprite = eiValittu;
    }
    public void JakolaskutValittu()
    {
        if (jakoLaskut && (kertoLaskut || yhteenLaskut))
        {
            JakolaskutPois();
        }
        else
        {
            PlayerPrefs.SetInt("jakoLaskut", 1);
            jakoLaskut = true;
            jakolaskuNappi.sprite = valittu;
        }
        
    }
    public void JakolaskutPois()
    {
        PlayerPrefs.SetInt("jakoLaskut", 0);
        jakoLaskut = false;
        jakolaskuNappi.sprite = eiValittu;

    }

}

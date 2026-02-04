using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class VoittoTahdetYleinen : MonoBehaviour
{
    public string pelinNimi; //tätä käytetään playerprefsin tallentamiseen, pitää olla uniikki, mieluiten projektin nimi esim VuohiPeli
    public GameObject harjoittele;
    public string valikkoScenenNimi; // tähän sceneen siirrytään kun pelaaja klikkaa voittoruutua tai tietyn ajan kuluttua
    public static int pisteet; //tallentaa unityn playerprefs systeemillä highscoren ja asettaa pistetauluu sekä määrää kuinka monta tähteä

    public int yksiTahtiRaja; //säädä nämä peliin sopivaksi
    public int kaksiTahtiRaja;
    static int julkinenTahtiRaja;
    public int kolmeTahtiRaja;

    public TextMeshProUGUI pisteTeksi;
    public TextMeshProUGUI parhaatPisteet;
    bool sammuta;
    public bool sammuttaaPelin = true; // saako voitto objekti siirtää pelaajan valikkoon


    // Start is called before the first frame update
    private void OnEnable() //VOITTO RUUTU AKTIVOIDAAN AKTIVOIMALLA PELIOBJEKTI! GameObjecSetActive(true)
    {
        pelinNimi = Pelikohtaiset.pelinNimi;
        print("pelion nimi on:  " + Pelikohtaiset.pelinNimi);
        GetComponent<Animator>().Play("Alku");
        Debug.Log("pelaaja sai " + pisteet + " pistettä");
        if (pelinNimi == null)
        {
            Debug.LogError("Laita Voitto Objektin scriptiin oikea pelin nimi");
        }

        if (pisteet < yksiTahtiRaja)
        {
            GetComponent<Animator>().Play("Tahdet0");
        }
        if (pisteet >= yksiTahtiRaja && pisteet < kaksiTahtiRaja)
        {
            GetComponent<Animator>().Play("Tahdet1");
        }
        if (pisteet >= kaksiTahtiRaja && pisteet < kolmeTahtiRaja)
        {
            GetComponent<Animator>().Play("Tahdet2");
        }
        if (pisteet >= kolmeTahtiRaja)
        {
            GetComponent<Animator>().Play("Tahdet3");
        }
        Invoke("Harjoittele", 3);


        AsetaPisteet();

        if (pelinNimi != "PommiPeli")
        {

            Invoke("Lopeta", 25);
        }
        Invoke("SaaSammuttaa", 2);
        if (sammuttaaPelin) VastausKontrolleri.Instance.PiilotaKysymys();
    }
    void SaaSammuttaa()
    {
        sammuta = true;
        if (GameObject.Find("VastauksenVahvistaja") != null)
        {
            GameObject.Find("VastauksenVahvistaja").GetComponent<Animator>().Play("Piilossa");
        }
    }
    private void Update()
    {
        if ((Input.GetKey(KeyCode.Escape) || Input.GetMouseButton(0)) && sammuta)
        {
            if (pelinNimi != "PommiPeli")
            {
                Lopeta();
            }
        }
    }

    public void AsetaPisteet()
    {
        if (pisteet > PlayerPrefs.GetInt(Pelikohtaiset.pelinNimi + "ParhaatPisteet" + RyhmanHighScoret.ryhmaId))
        {
            PlayerPrefs.SetInt(Pelikohtaiset.pelinNimi + "ParhaatPisteet" + RyhmanHighScoret.ryhmaId, pisteet);
        }

        parhaatPisteet.text = PlayerPrefs.GetInt(Pelikohtaiset.pelinNimi + "ParhaatPisteet" + RyhmanHighScoret.ryhmaId).ToString();
        pisteTeksi.text = pisteet.ToString();
    }

    void Harjoittele()
    {
        harjoittele.SetActive(true);
        //GetComponent<Animator>().Play("LaatikonTulo");
    }
    public void Lopeta()
    {

        if (sammuttaaPelin)
        {

            harjoittele.SetActive(false);
            if (GameObject.Find("Musiikki") != null)
            {
                GameObject.Find("Musiikki").GetComponent<Musiikki>().PalautaMusa();
            }

            PeliManageri.SiirryValikkoon();
        }




    }
}

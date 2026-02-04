using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class MoninPeliVoitto : MonoBehaviour
{
   
    public MultiplayerClient.Player pelaajaVoittaja;
    public GameObject mPObj;
    MoninPeli mP;
    MultiplayerClient mpc;
    int voittajanPisteet;
    public TextMeshProUGUI voittajanNimiTeksti;
    public TextMeshProUGUI voittajanPisteTeksti;
    public GameObject moninpelinappi;


    private void OnEnable()
    {
        TallennaPisteet();
        voittajanPisteet = 0;
        if (mPObj != null)
        {
            if (mPObj.TryGetComponent<MoninPeli>(out mP))
            {
                MoninPeli.Pelaaja voittaja = null; 
                foreach (var pelaaja in mP.pelaajatNyt.pelaajat)
                {
                    if (pelaaja.pisteet > voittajanPisteet)
                    {
                        voittajanPisteet = pelaaja.pisteet;
                        voittaja = pelaaja;
                    }
                }
                voittajanNimiTeksti.text = voittaja.nimi;
                voittajanPisteTeksti.text = voittaja.pisteet.ToString();
            }
            if (mPObj.TryGetComponent<MultiplayerClient>(out mpc))
            {
                MultiplayerClient.Player voittaja = null;
                foreach (var pelaaja in mpc.networkPlayers.players)
                {
                    if (pelaaja.score > voittajanPisteet)
                    {
                        voittajanPisteet = pelaaja.score;
                        voittaja = pelaaja;
                    }
                }
                voittajanNimiTeksti.text = voittaja.name;
                voittajanPisteTeksti.text = voittaja.score.ToString();
            }
        }

        else
        {
            voittajanNimiTeksti.text = pelaajaVoittaja.name;
            voittajanPisteTeksti.text = pelaajaVoittaja.score.ToString();
        }

      

        Invoke("Valikkoon", 4);
    }

    public void TallennaPisteet()
    {
        if (Pelikohtaiset.pisteet > PlayerPrefs.GetInt(Pelikohtaiset.pelinNimi + "ParhaatPisteet" + RyhmanHighScoret.ryhmaId))
        {
            PlayerPrefs.SetInt(Pelikohtaiset.pelinNimi + "ParhaatPisteet" + RyhmanHighScoret.ryhmaId, Pelikohtaiset.pisteet);
        }
    }

    void Valikkoon()
    {
       // moninpelinappi.SetActive(true);
        PeliManageri.SiirryValikkoon();
        gameObject.SetActive(false);
        if (moninpelinappi != null)
        {
            moninpelinappi.SetActive(true);
        }
       
    }
}

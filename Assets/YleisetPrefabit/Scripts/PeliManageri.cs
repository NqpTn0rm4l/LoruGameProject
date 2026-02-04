using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using System;

public class PeliManageri : MonoBehaviour
{
    public UnityEvent peliin;
    public UnityEvent valikkoon;
    public string peliScenenNimi = "peli";

    public Boolean peliOnAinaLandscapessa = true;
    // Start is called before the first frame update


    void Start()
    {
        Pelikohtaiset.valikkoNimi = SceneManager.GetActiveScene().name;
        Pelikohtaiset.pelinNimi = peliScenenNimi;
        Pelikohtaiset.onAinaLandscape = peliOnAinaLandscapessa;
        if (!peliOnAinaLandscapessa)
        {
            DataManager.Instance.LoruNet.BrowserDispatch(Commands.LogMsg, "portraitAvailable");
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
    public static void SiirryPeliin()
    {
        DataManager.Instance.LoruNet.BrowserDispatch(Commands.LogMsg, "MoveToGame");
        SceneManager.LoadScene(Pelikohtaiset.pelinNimi);
        GameObject.Find("PeliManageri").GetComponent<PeliManageri>().peliin.Invoke();
        if (GameObject.Find("YleisetAlussa") != null)
        {
            GameObject.Find("YleisetAlussa").GetComponent<Musiikki>().HiljennaMusa();
        }
    }


    public static async void SiirryValikkoon()
    {
        GameObject.Find("PeliManageri").GetComponent<PeliManageri>().valikkoon.Invoke();
        SceneManager.LoadScene(Pelikohtaiset.valikkoNimi);
        DataManager.Instance.LoruNet.BrowserDispatch(Commands.LogPlayerAction, "MoveToMenu");

        if (GameObject.Find("YleisetAlussa") != null)
        {
            GameObject.Find("YleisetAlussa").GetComponent<Musiikki>().PalautaMusa();
        }
    }


    public static void Poistu()
    {
        DataManager.Instance.LoruNet.BrowserBack();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.Networking;

public class VastausKontrolleri : MonoBehaviour
{
    /* Käyttöohje:
     * Aseta kaikkiin vastausvaihtoehto objekteihin tagi Vastaus (tämä voi olla myös child)
     * Tämän sciptin asetus 1 etsii pelaajaa lähimmät 4, 3 tai 2 vastausobjektia
     * randomisoi niiden järjestyksen ja liittää niihin vastausscriptit jotka 
     * asettavat mikä on oikea ja mikä väärä
     * 
     * Luo projektiisi tägit: Vastaus ja Vastattu
     * 
     * Muuttaaksesi tapahtumia, joita pelissäsi tapahtuu pelaajan vastattua oikein/väärin muokkaa Pelikohtaiset scriptiä
     * 
     * Ensin tämä skripti valitsee Tehtavat skriptistä sopivan kysymyksen. Valinnassa painotetaan tehtäviä joiden osaamistaso on heikompi
     * 
     * Pelistä kutsuttavat tapahtumat:
     * -UusiTehtava() kun scenessä on olemassa objektit joiden tagi on "Vastaus"
     * -Vastaa("Oikein") tai Vastaa() silloin kun pelaaja osuu kohdeobjektiin (vastauksen voi myös vaatia kirjoittamaan pelissä)
     * 
     * Tärkeät datasijainnit:
     * TehtavaLuokka määrittelee mitä tietoja tehtävän alle voi tulla
     * Tehtavat.sc List<TehtavaLuokka> tehtavatTassaSessiossa säilyttää tehtävät session ajan, sieltä peli hakee ja sinne syöttää
     * Tämän scritpin tehtavaNyt jossa on tallessa juuri sillä hetkellä pelattava tehtävä.
     * 
     * Tärkeät
     * 
     * TODO: Mitä jos monta tehtävää pitää näkyä yhtäaikaa? --> Hae lähimmät kohtaan tagin poisto?
     * TODO: Uusien vastausten hakeminen skeneen rikkoo pelin jos scenessä ei ole neljää Vastaus tagilla olevaa objektia
     * 
     */


    [SerializeField]
    public List<TehtavaLuokka> nakyy;

    public int vaihtoehtojenMaara = 4;
    public GameObject pelaaja;
    public static GameObject[] vastausObjektit;
    public static GameObject kysymysObjekti;
    public GameObject kiinteaKysymysObjekti;
    public bool pysyvaKysymysTeksti;
    public bool pysyvatVastausObjektit;
    public bool alaNimeaVastausobjekteja; //nimeää vastausobjektit "vastattu" 
    bool tehtavaObjektitHaettu;
    public bool voiArvataMontaKertaa;

    //testausnapit
    public bool nappiUusiTehtava;
    public GameObject kuvaKysymysObj;

    //kuva kysymys
    Texture kysymyKuvaTex;
    RawImage uiKymysKuva;
    MeshRenderer meshKysymysKuva;
    float kuvaKorkeus;


    public static TehtavaLuokka tehtavaNyt; // tämä muuttuja sisältää kaiken tiedon tämän hetkisestä tehtävästä

    static int tehtavaNytIndeksi;
    static Pelikohtaiset pelikohtaiset;

    public static string[] vaaratVaihtoehdot = new string[3];

    GameObject vastauksenVahvistus;


    private static VastausKontrolleri _instance;
    public static VastausKontrolleri Instance { get { return _instance; } }
    private void Awake()
    {

        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    void Start()
    {
        //kokeillan onko pelikohtaiset skripti objektilla, jos ei lisätään
        if (!TryGetComponent<Pelikohtaiset>(out Pelikohtaiset comp))
        {
            pelikohtaiset = gameObject.AddComponent<Pelikohtaiset>();
        }

        if (VastauksenVahvistaja.instance != null)
        {
            vastauksenVahvistus = VastauksenVahvistaja.instance.gameObject;
        }
    }

    private void Update()
    {
        if (nappiUusiTehtava)
        {
            UusiTehtava();
            nappiUusiTehtava = !nappiUusiTehtava;
        }

    }
    /*Tarvitaanko jonkinlainen switch joka vaihtaa eri tapojen välillä tai eri funktiot eri tavoile:
     * 1. Valitse lähimmät objektit tagin perusteella Vastaus 4kpl ja tee niistä
     * 2. Funktio johon voi syöttää oikeat 4 GameObjektia, tekee niistä vastauspalikoita
     * 
     */

    // tämä funktio kokoaa kaikki tehtävän uusimiseen tarvittavat
    public void UusiTehtava()
    {

        if (GameObject.Find("DataManager") != null)
        {
            if (GameObject.Find("DataManager").GetComponent<TehtavatKartoitus>().tehtavat.Count < 1)
            {
                transform.GetChild(0).transform.Find("VikaEiKysymyksia").gameObject.SetActive(true);
            }
            else
            {
                if (!pysyvatVastausObjektit)
                {
                    if (GameObject.FindGameObjectsWithTag("Vastaus").Length == 0)
                    {
                        print("Tyhjiä vastausobjekteja ei löydy. Lisää ne ja varmista että niillä on Vastaus tagi");
                    }
                    else
                    {
                        vastausObjektit = ListaaLahimmat();
                    }
                }
                else
                {

                    if (!tehtavaObjektitHaettu)
                    {

                        vastausObjektit = ListaaLahimmat();
                        tehtavaObjektitHaettu = true;

                    }
                    else
                    {
                        foreach (var item in vastausObjektit)
                        {

                            item.name = "VastausObjekti";
                            item.tag = "Vastaus";
                        }
                    }


                }

                if (pysyvaKysymysTeksti)
                {
                    kysymysObjekti = kiinteaKysymysObjekti;
                }
                else
                {
                    kysymysObjekti = ValitseLahinKysymysObjekti();
                }
                VaihdaTehtavaa(vastausObjektit, kysymysObjekti);
            }

        }
        else
        {
            Debug.LogWarning("Laita Tehtavat Scripti johonkin objektiin, että saadaan serveriltä tehtävät");
        }

    }

    public void PiilotaKysymys()
    {
        if (pysyvaKysymysTeksti)
        {
            kysymysObjekti = kiinteaKysymysObjekti;
        }
        else
        {
            kysymysObjekti = ValitseLahinKysymysObjekti();
        }
        kysymysObjekti.SetActive(false);
    }

    public async void VaihdaTehtavaa(GameObject[] vastausVaihtoehdot = null, GameObject kysymysObjekti = null, TehtavaLuokka teht = null)
    {
        if (vastausVaihtoehdot != null)
        {
            vastausObjektit = vastausVaihtoehdot;
        }


        if (teht == null)
        {
            tehtavaNyt = ValitseSopivaTehtava();
        }
        else
        {
            tehtavaNyt = teht;
        }

        //lähetetään kysymysobjektille tämänhetkisen tehtävän teksti tai jos url niin kuva


        if (kysymysObjekti == null)
        {
            print("Tyhjiä Kysymysobjekteja. Lisää ne ja varmista että niillä on Kysymys tagi, sekä tekstikomponentti");
        }

        Uri uriResult;
        bool kysymysOnUrl = Uri.TryCreate(tehtavaNyt.tehtavaKysymys, UriKind.Absolute, out uriResult)
            && uriResult.Scheme == Uri.UriSchemeHttp;

        if (System.Text.RegularExpressions.Regex.IsMatch(tehtavaNyt.tehtavaKysymys, @"(https?:\/\/[^ ]*\.(?:gif|png|jpg|jpeg))"))
        {
            kysymysOnUrl = true;
        }

        if (kysymysOnUrl)
        {
            kuvaKorkeus = kysymysObjekti.GetComponent<RectTransform>().rect.height;
            //jos kuvia on haettu aiemmin on objektissa jo child
            if (kysymysObjekti.transform.childCount > 0)
            {

                if (kysymysObjekti.GetComponent<TextMeshPro>() != null)
                {

                    if (kysymysObjekti.transform.Find("KuvaKysymys") != null)
                    {

                        print("löyty");
                        //objektin childissa on jo image komponentti
                        //return;
                    }
                    else
                    {
                        GameObject kuvaKysymys = new GameObject();
                        kuvaKysymys.name = "KuvaKysymys";
                        kuvaKysymys.transform.SetParent(kysymysObjekti.transform);
                        meshKysymysKuva = kuvaKysymys.AddComponent<MeshRenderer>();

                    }



                }
                if (kysymysObjekti.GetComponent<TextMeshProUGUI>() != null)
                {

                    if (kysymysObjekti.transform.Find("KuvaKysymys") != null)
                    {
                        if (kysymysObjekti.transform.Find("KuvaKysymys").TryGetComponent(out uiKymysKuva))
                        {

                            //objektin childissa on jo image komponentti
                            //return;


                        }
                    }
                    else
                    {

                        GameObject kuvaKysymys = new GameObject();
                        kuvaKysymys.name = "KuvaKysymys";
                        kuvaKysymys.transform.SetParent(kysymysObjekti.transform);
                        uiKymysKuva = kuvaKysymys.AddComponent<RawImage>();
                        //uiKymysKuva.rectTransform.sizeDelta = new Vector2(kuvaKorkeus, kuvaKorkeus);
                    }

                }
            }
            else
            {
                if (kysymysObjekti.GetComponent<TextMeshPro>() != null)
                {
                    GameObject kuvaObj = Instantiate(kuvaKysymysObj, kysymysObjekti.transform);
                    meshKysymysKuva = kuvaObj.GetComponent<MeshRenderer>();

                }
                if (kysymysObjekti.GetComponent<TextMeshProUGUI>() != null)
                {
                    GameObject kuvaObj = Instantiate(new GameObject(), kysymysObjekti.transform);
                    kuvaObj.name = "KuvaKysymys";
                    kuvaObj.transform.localScale = new Vector3(1.3f, 1.3f, 1.3f);
                    if (kysymysObjekti.transform.GetChild(0).TryGetComponent(out uiKymysKuva))
                    {

                        return;
                    }
                    else
                    {

                        uiKymysKuva = kysymysObjekti.transform.GetChild(0).gameObject.AddComponent<RawImage>();
                        uiKymysKuva.rectTransform.sizeDelta = new Vector2(kuvaKorkeus, kuvaKorkeus);
                    }

                }
            }

            //Poistaa kysymystekstin
            if (kysymysObjekti.GetComponent<TextMeshPro>() != null)
            {
                kysymysObjekti.GetComponent<TextMeshPro>().text = "";
                StartCoroutine(LataaKuva(tehtavaNyt.tehtavaKysymys, meshKysymysKuva));

            }
            if (kysymysObjekti.GetComponent<TextMeshProUGUI>() != null)
            {
                kysymysObjekti.GetComponent<TextMeshProUGUI>().text = "";
                StartCoroutine(LataaKuva(tehtavaNyt.tehtavaKysymys, null, uiKymysKuva));
            }


        }
        else
        {
            //ei ole kuva url kysymyksessä
            kysymyKuvaTex = null;
            if (meshKysymysKuva != null)
            {
                meshKysymysKuva.material.mainTexture = null;
            }
            if (uiKymysKuva != null)
            {
                uiKymysKuva.enabled = false;
            }


            //jos texmesh komponetti
            if (kysymysObjekti.GetComponent<TextMeshPro>() != null)
            {
                kysymysObjekti.GetComponent<TextMeshPro>().text = tehtavaNyt.tehtavaKysymys;
                if (kysymysObjekti.GetComponent<MeshRenderer>().sortingOrder < 10)
                {
                    kysymysObjekti.GetComponent<MeshRenderer>().sortingOrder = 10;
                }
            }
            if (kysymysObjekti.GetComponent<TextMeshProUGUI>() != null)
            {
                kysymysObjekti.GetComponent<TextMeshProUGUI>().text = tehtavaNyt.tehtavaKysymys;
            }
        }

        if (kysymysObjekti.GetComponent<Text>() == null && kysymysObjekti.GetComponent<TextMeshPro>() == null && kysymysObjekti.GetComponent<TextMeshProUGUI>() == null)
        {
            Debug.LogError("Lisää kysymysobjektiin Textmesh pro komponentti tai ui text");
        }

        // sekoittaa objektien järjestyksen
        for (int i = 0; i < vastausVaihtoehdot.Length; i++)
        {
            GameObject temp = vastausVaihtoehdot[i];
            int randomIndex = UnityEngine.Random.Range(i, vastausVaihtoehdot.Length);
            vastausVaihtoehdot[i] = vastausVaihtoehdot[randomIndex];
            vastausVaihtoehdot[randomIndex] = temp;
        }

        VastausTekstinAsettaja.kaikkiTekstit = new List<TextMeshPro>();
        VastausTekstinAsettaja.kaikkiTekstitGUI = new List<TextMeshProUGUI>();

        for (int i = 0; i < vastausVaihtoehdot.Length; i++)
        {
            if (vastausVaihtoehdot[i].GetComponent<VastausTekstinAsettaja>() == null)
            {
                vastausVaihtoehdot[i].AddComponent<VastausTekstinAsettaja>();
            }
            vastausVaihtoehdot[i].GetComponent<VastausTekstinAsettaja>().mikaVastaus = i;
            vastausVaihtoehdot[i].GetComponent<VastausTekstinAsettaja>().AsetaTeksti();
        }

        Invoke("SkaalaTekstit", 0.1f);


    }
    void SkaalaTekstit()
    {
        float fontMaksKoko = 40;
        TextMeshPro tmp;
        if (vastausObjektit.Length < 2)
        {
            Debug.LogWarning("Vastausvaihtoehtojen määrä liian pieni");
            return;
        }
        if (vastausObjektit[0].TryGetComponent(out tmp) || vastausObjektit[0].transform.GetChild(0).TryGetComponent(out tmp))
        {
            foreach (var go in vastausObjektit)
            {
                TextMeshPro tm = go.GetComponentInChildren<TextMeshPro>();

                if (tm.fontSize < fontMaksKoko)
                {
                    fontMaksKoko = tm.fontSize;
                }
            }
            foreach (var go in vastausObjektit)
            {
                TextMeshPro tm = go.GetComponentInChildren<TextMeshPro>();
                tm.fontSize = fontMaksKoko;
                tm.enableAutoSizing = false;
            }
        }
        else
        {
            foreach (var go in vastausObjektit)
            {
                TextMeshProUGUI tm = go.GetComponentInChildren<TextMeshProUGUI>();
                if (tm != null)
                {
                    if (tm.fontSize < fontMaksKoko)
                    {
                        fontMaksKoko = tm.fontSize;
                    }
                }
            }
            foreach (var go in vastausObjektit)
            {

                TextMeshProUGUI tm = go.GetComponentInChildren<TextMeshProUGUI>();
                tm.fontSize = fontMaksKoko;
                tm.enableAutoSizing = false;
            }
        }
    }



    // Listaa kaikki tehtava tagilla olevat objektit ja palauta niistä 4 lähintä
    GameObject[] ListaaLahimmat()
    {

        if (vaihtoehtojenMaara > GameObject.FindGameObjectsWithTag("Vastaus").Length)
        {
            vaihtoehtojenMaara = GameObject.FindGameObjectsWithTag("Vastaus").Length;
        }
        GameObject[] lahimmat = new GameObject[vaihtoehtojenMaara];
        List<GameObject> kaikkivastausObjektit = new List<GameObject>();
        GameObject[] kaikki = GameObject.FindGameObjectsWithTag("Vastaus");

        foreach (GameObject go in kaikki)
        {
            kaikkivastausObjektit.Add(go);
            //Debug.Log(go);
        }
        //järjestetään lähimmät ensin
        kaikkivastausObjektit.Sort(ByDistance);


        int ByDistance(GameObject a, GameObject b)
        {
            var dstToA = Vector3.Distance(pelaaja.transform.position, a.transform.position);
            var dstToB = Vector3.Distance(pelaaja.transform.position, b.transform.position);
            return dstToA.CompareTo(dstToB);
        }

        for (int i = 0; i < vaihtoehtojenMaara; i++)
        {
            lahimmat[i] = kaikkivastausObjektit[i];
            //Debug.Log("tehtavaobjekti löydetty: " + kaikkivastausObjektit[i]);
        }
        return lahimmat;
    }

    GameObject ValitseLahinKysymysObjekti()
    {
        GameObject lahin = null;
        float etaisyys = Mathf.Infinity; ;
        Vector3 pelaajanSijainti = pelaaja.transform.position;
        GameObject[] kysymykset = GameObject.FindGameObjectsWithTag("Kysymys");
        for (int i = 0; i < vaihtoehtojenMaara; i++)
        {
            foreach (GameObject go in kysymykset)
            {
                float etaisyysNyt = Vector3.Distance(go.transform.position, pelaajanSijainti);
                if (etaisyysNyt < etaisyys)
                {
                    lahin = go;
                    //Debug.Log(go);
                    etaisyys = etaisyysNyt;
                }
            }
        }
        return lahin;
    }

    // Etsi lähin neljä lähintä vastausobjektia
    public GameObject[] Lahimmat()
    {
        GameObject[] lahimmat = new GameObject[vaihtoehtojenMaara];
        for (int i = 0; i < vaihtoehtojenMaara; i++)
        {
            GameObject go = EtsiLahinVastaus(GameObject.FindGameObjectsWithTag("Vastaus"));
            go.tag = "Valittu";
            Debug.Log("pääsi");
            Debug.Log(go);
            lahimmat[0] = go;
        }
        return lahimmat;
    }

    GameObject EtsiLahinVastaus(GameObject[] vastaukset)
    {
        GameObject lahin = null;
        float etaisyys = Mathf.Infinity; ;
        Vector3 pelaajanSijainti = pelaaja.transform.position;
        for (int i = 0; i < vaihtoehtojenMaara; i++)
        {
            foreach (GameObject go in vastaukset)
            {
                float etaisyysNyt = Vector3.Distance(go.transform.position, pelaajanSijainti);
                if (etaisyysNyt < etaisyys)
                {
                    lahin = go;
                    //Debug.Log(go);
                    etaisyys = etaisyysNyt;
                }
            }
        }
        return lahin;
    }

    // tässä valitaan tehtäväNyt objektiin sopiva tethtävä joka sitten syötetään peliobjekteihin
    public TehtavaLuokka ValitseSopivaTehtava()
    {
        Tehtavat.tehtavatTassaSessiossa.Sort((x, y) => x.osaamisTaso.CompareTo(y.osaamisTaso));
        tehtavaNytIndeksi = UnityEngine.Random.Range(0, 5);
        while (Tehtavat.tehtavatTassaSessiossa[tehtavaNytIndeksi] == tehtavaNyt)
        {
            tehtavaNytIndeksi = UnityEngine.Random.Range(0, 5);
        }
        nakyy = Tehtavat.tehtavatTassaSessiossa;

        //arvotaan mitkä sopisivat vääriin vaihtoehtoihin siltä varalta että settiin ei ole määritelty vääriä
        List<TehtavaLuokka> tmp = EB.Utils.DeepClone(nakyy);
        List<TehtavaLuokka> tmp2 = new List<TehtavaLuokka>();

        foreach (var tehtava in tmp)
        {
            if (tehtava.settiID == tmp[tehtavaNytIndeksi].settiID)
            {
                tmp2.Add(tehtava);
                // Debug.Log("poistan " + tehtava.tehtavaKysymys);
            }

        }
        tmp2.RemoveAt(tehtavaNytIndeksi);
        int laskuri = 0;
        for (int i = 0; i < 3; i++)
        {

            int rnd = UnityEngine.Random.Range(0, tmp2.Count);
            while (tmp2[rnd].oVastaus == nakyy[tehtavaNytIndeksi].oVastaus)
            {
                rnd = UnityEngine.Random.Range(0, tmp2.Count);
                laskuri++;
                if (laskuri > 1000)
                {
                    break;
                }
            }
            vaaratVaihtoehdot[i] = tmp2[rnd].oVastaus;
            tmp2.RemoveAt(rnd);

        }


        return Tehtavat.tehtavatTassaSessiossa[tehtavaNytIndeksi];

    }


    static TehtavaLuokka prevTehtävä;


    //tämä kutsutaan kun pelaajan on vastannut kysymykseen
    public static void Vastaa(string oikeinVaiVaarin, TehtavaLuokka vastattavaTehtava = null) // funktio joka säätää osaamistasoa
    {
        if (vastattavaTehtava != null)
        {
            tehtavaNyt = vastattavaTehtava;
        }
        if (prevTehtävä?.tehtäväId == tehtavaNyt.tehtäväId
            && prevTehtävä?.skillDataId == tehtavaNyt.skillDataId
            && prevTehtävä?.vastaamisKerrat == tehtavaNyt.vastaamisKerrat)
            throw new System.Exception("Toistuva vastaamiskerta Detected!");

        prevTehtävä = EB.Utils.DeepClone(tehtavaNyt);
        tehtavaNyt.vastaamisKerrat++;

        kysymysObjekti = GameObject.Find("VastausKontrolleri").GetComponent<VastausKontrolleri>().ValitseLahinKysymysObjekti();

        //muutetaan "käytettyjen" tehtäväobjektien nimet ja tägit ettei niihin enää tule uusia tehtäviä

        GameObject[] vastaukset = vastausObjektit;
        if (!GameObject.Find("VastausKontrolleri").GetComponent<VastausKontrolleri>().pysyvatVastausObjektit && !GameObject.Find("VastausKontrolleri").GetComponent<VastausKontrolleri>().voiArvataMontaKertaa)
        {
            for (int i = 0; i < vastaukset.Length; i++)
            {
                if (vastaukset[i] != null)
                {
                    vastaukset[i].name = "Vastattu " + i.ToString();
                    vastaukset[i].tag = "Vastattu";
                }
            }
        }
        else
        {
            if (!GameObject.Find("VastausKontrolleri").GetComponent<VastausKontrolleri>().alaNimeaVastausobjekteja)
            {
                foreach (var vObj in VastausKontrolleri.vastausObjektit)
                {
                    vObj.name = "Vastaus";
                    vObj.tag = "Vastaus";
                }
            }
        }

        if (!VastausKontrolleri.Instance.GetComponent<VastausKontrolleri>().pysyvaKysymysTeksti)
        {
            kysymysObjekti.tag = "Kysytty";
            kysymysObjekti.name = "Kysytty";
        }
        if (oikeinVaiVaarin == "Oikein")
        {
            tehtavaNyt.osaamisTaso++;

            //avataan vastauksen vahvistus
            if (VastausKontrolleri.Instance.GetComponent<VastausKontrolleri>().vastauksenVahvistus != null)
            {
                GameObject vastausVahvistus = VastausKontrolleri.Instance.GetComponent<VastausKontrolleri>().vastauksenVahvistus;
                VastauksenVahvistaja.Paalle();
                Transform kysymysVahvistus = vastausVahvistus.transform.Find("KysymysVahvistus");
                if (VastausKontrolleri.Instance.kysymyKuvaTex != null)
                {
                    kysymysVahvistus.GetComponent<TextMeshProUGUI>().text = "";
                    kysymysVahvistus.GetComponentInChildren<RawImage>().enabled = true;
                    kysymysVahvistus.GetComponentInChildren<RawImage>().texture = Instance.kysymyKuvaTex;
                    // kysymysVahvistus.GetComponentInChildren<RawImage>().rectTransform.sizeDelta = new Vector2(Instance.kysymyKuvaTex.height / 3, Instance.kysymyKuvaTex.width / 3);
                }
                else
                {
                    kysymysVahvistus.GetComponentInChildren<RawImage>().enabled = false;
                    kysymysVahvistus.GetComponent<TextMeshProUGUI>().text = tehtavaNyt.tehtavaKysymys;
                }
                vastausVahvistus.transform.Find("Vastaus").GetComponent<TextMeshProUGUI>().text = tehtavaNyt.oVastaus;

            }
            //pelikohtaiset.PelaajaVastasiOikein();
            //float kerrat = tehtavaNyt.vastaamisKerrat;
            //tehtavaNyt.osaamisNopeusTaso = (kerrat * tehtavaNyt.osaamisNopeusTaso + nopeus) / (kerrat + 1f); //miteti miten nopeus kirjataan vai kirjataanko
        }
        else
        {
            tehtavaNyt.osaamisTaso--;
            //pelikohtaiset.PelaajaVastasiVaarin();
        }

        //tallennetaan uusi data mm osaamistaso talteen sessiokohtaisiin tietoihin.
        Tehtavat.tehtavatTassaSessiossa[tehtavaNytIndeksi] = tehtavaNyt;
        //lähetetään serverille vastaus
        DataManager.LähetäPelaajanVastausData(tehtavaNyt);
    }

    IEnumerator LataaKuva(string MediaUrl, MeshRenderer meshKuva = null, RawImage uiKuva = null)
    {
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(MediaUrl);
        yield return request.SendWebRequest();
        if (request.isNetworkError || request.isHttpError)
            Debug.Log(request.error);
        else
            kysymyKuvaTex = ((DownloadHandlerTexture)request.downloadHandler).texture;
        if (meshKuva != null)
        {
            //pitää säätää kuvan skaalautuminen kun kyseessä on sprite
            //Texture2D kysKuva = (Texture2D)kysymyKuvaTex;

            meshKuva.material.mainTexture = kysymyKuvaTex;
        }
        if (uiKymysKuva != null)
        {
            uiKuva.enabled = true;
            uiKuva.texture = kysymyKuvaTex;
        }
    }
}






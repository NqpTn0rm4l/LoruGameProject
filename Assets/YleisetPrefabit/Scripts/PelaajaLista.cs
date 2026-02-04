using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class PelaajaLista : MonoBehaviour
{

    public GameObject pelaajaIkoniPrefab;
    List<GameObject> pelaajaIkonit;
    List<GameObject> pelaajaObjektit = new List<GameObject>();
    public int pelaajienMaara;
    public List<Sprite> ikonit;
    public List<MoninPeli.Pelaaja> pelaajat;
    // Start is called before the first frame update


    public void UusiMoninPeli()
    {
        print("pelaajamaara nollattu");
        pelaajienMaara = 0;
        print(pelaajienMaara);
        if (pelaajaObjektit.Count>0)
        {
            foreach (var item in pelaajaObjektit)
            {
                Destroy(item);
            }
            pelaajat.Clear();
        }
    }


    public void PoistaPelaaja(int pelaajaId)
    {
        print("Poistan pelaajan listaanm: " + pelaajaId);

        GameObject poistettava = pelaajaObjektit.Find(x => x.name == pelaajaId.ToString());

        pelaajat.Remove(pelaajat.Find(x => x.id == pelaajaId));


        pelaajienMaara--;

    }

    public void LisaaPelaaja(MoninPeli.Pelaaja pelaaja)
    {
        print("Lisanpelaajan listaanm: " + pelaaja.id);
        pelaajat.Add(pelaaja);
        GameObject klooni = Instantiate(pelaajaIkoniPrefab,transform.GetChild(0).position + Vector3.left*pelaajienMaara*90,Quaternion.identity);
        pelaajaObjektit.Add(klooni);
        klooni.GetComponentInChildren<TextMeshProUGUI>().text = pelaaja.nimi;
        klooni.GetComponentInChildren<Image>().sprite = ikonit[pelaaja.hahmo];
        klooni.name = pelaaja.id.ToString();
        klooni.transform.SetParent(transform);
        pelaajienMaara++;
        
    }

    public void PaivitaPelaajat()
    {
        foreach (var item in MoninPeli.instance.pelaajatNyt.pelaajat)
        {
            GameObject tmp = pelaajaObjektit.Find(x => x.name == item.id.ToString());
          
            if (tmp == null)
            {
                LisaaPelaaja(item);
            }

        }
        GameObject toDestroy = null;
        foreach (var item in pelaajaObjektit)
        {
            MoninPeli.Pelaaja pel = MoninPeli.instance.pelaajatNyt.pelaajat.Find(x => x.id == int.Parse(item.name));
            if (pel == null)
            {
                toDestroy = item;
            }
            else
            {              
                item.GetComponentInChildren<TextMeshProUGUI>().text = pel.nimi;
                item.GetComponentInChildren<Image>().sprite = ikonit[pel.hahmo];
            }

        }
        if (toDestroy != null)
        {
            pelaajaObjektit.Remove(toDestroy);
            Destroy(toDestroy);
            pelaajienMaara--;
        }
    
    }
}

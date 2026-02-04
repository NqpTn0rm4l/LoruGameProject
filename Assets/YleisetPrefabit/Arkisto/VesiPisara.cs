using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VesiPisara : MonoBehaviour
{

    Text vesiTeksti;

    private void Start()
    {
        vesiTeksti = GameObject.Find("VesiTeksti").GetComponent<Text>();
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        GetComponent<BoxCollider2D>().enabled = false;
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<Animator>().enabled = false;
        GetComponent<ParticleSystem>().Play();
        LisaaVesiPiste();
        PaivitaVesiTeksti();
        Invoke("Tuhoa", 2);
    }

    void LisaaVesiPiste()
    {
        Tehtavat.kayttajanTiedotTassaSessiossa.vedenMaara++;
    }

    void PaivitaVesiTeksti()
    {
        vesiTeksti.text = Tehtavat.kayttajanTiedotTassaSessiossa.vedenMaara.ToString();
    }

    void Tuhoa()
    {
        Destroy(gameObject);
    }
}

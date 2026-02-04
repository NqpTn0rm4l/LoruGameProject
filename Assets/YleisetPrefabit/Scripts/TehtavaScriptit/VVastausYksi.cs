using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VVastausYksi : MonoBehaviour
{
    /* Tämä scripti liitetään väärävastaus 1 objektiin jolla on collideri
     * Scripti laittaa objektille tagin VVastaus1, tästä pelaajaobjekti tunnistaa 
     * törmäyksessä, että vastaus oli väärä
     * */


  

    void Start()
    {
        gameObject.name = "VVastausYksi";

        GetComponent<VastausTekstinAsettaja>().AsetaTeksti();
        
    }

}

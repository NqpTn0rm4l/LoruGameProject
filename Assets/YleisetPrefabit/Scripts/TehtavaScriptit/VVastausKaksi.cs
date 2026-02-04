using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VVastausKaksi : MonoBehaviour
{
    /* Tämä scripti liitetään väärävastaus 2 objektiin jolla on collideri
  * Scripti laittaa objektille tagin VVastaus2, tästä pelaajaobjekti tunnistaa 
  * törmäyksessä, että vastaus oli väärä
  * */
   
    void Start()
    {
        gameObject.name = "VVastausKaksi";
       
        GetComponent<VastausTekstinAsettaja>().AsetaTeksti();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

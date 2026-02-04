using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VVastausKolme : MonoBehaviour
{
    /* Tämä scripti liitetään väärävastaus 3 objektiin jolla on collideri
   * Scripti laittaa objektille tagin VVastaus3, tästä pelaajaobjekti tunnistaa 
   * törmäyksessä, että vastaus oli väärä
   * */
    
    
    void Start()
    {
        gameObject.name = "VVastausKolme";
        
       
        GetComponent<VastausTekstinAsettaja>().AsetaTeksti();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

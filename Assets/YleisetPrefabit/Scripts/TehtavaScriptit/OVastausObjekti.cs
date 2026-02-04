using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OVastausObjekti : MonoBehaviour
{
    /* Tämä scripti liitetään Oikea vastaus objektiin jolla on collideri
   * Scripti laittaa objektille OVastaus, tästä pelaajaobjekti tunnistaa 
   * törmäyksessä, että vastaus oli oikea.
   * */
  
    
    void Start()
    {
        gameObject.name = "OVastaus";

        GetComponent<VastausTekstinAsettaja>().AsetaTeksti();
        
    }

  
}

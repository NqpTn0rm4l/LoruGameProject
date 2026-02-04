using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ParhaatPisteet : MonoBehaviour
{
    public List<GameObject> tahdet;

    TextMeshProUGUI tmp;

  
    void Start()
    {
        Invoke("Pisteet", 1.1f);
        
       tmp = GetComponent<TextMeshProUGUI>();
        
    }
    void Pisteet()
    {
        tmp.text = PlayerPrefs.GetInt(Pelikohtaiset.pelinNimi + "ParhaatPisteet" + RyhmanHighScoret.ryhmaId).ToString();
    }
}

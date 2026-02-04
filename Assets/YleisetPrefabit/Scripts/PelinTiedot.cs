using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PelinTiedot : MonoBehaviour
{
    public string pelinNimi;
     
    private void OnEnable()
    {

        Pelikohtaiset.pelinNimi = pelinNimi;
    }
}

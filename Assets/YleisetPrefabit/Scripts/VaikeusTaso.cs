using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VaikeusTaso : MonoBehaviour
{
    public static int taso = 1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public void AsetaTaso(int tmptaso)
    {
        taso = tmptaso;
        gameObject.SetActive(false);
    }
    public void Palauta()
    {
        gameObject.SetActive(true);
    }
}

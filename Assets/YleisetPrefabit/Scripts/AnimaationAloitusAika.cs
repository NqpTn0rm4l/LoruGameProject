using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimaationAloitusAika : MonoBehaviour
{
    public float aloitusAika;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Animator>().enabled = false;
        Invoke("Aloita", aloitusAika);
    }

    // Update is called once per frame
    void Aloita()
    {
        GetComponent<Animator>().enabled = true;
    }
}

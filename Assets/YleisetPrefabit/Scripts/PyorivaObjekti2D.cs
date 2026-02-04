using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PyorivaObjekti2D : MonoBehaviour
{
    public float nopeus = 1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(transform.forward, nopeus);
    }
}

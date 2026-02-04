using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MalliPelaaja : MonoBehaviour
{
    Quaternion alkuRotaatio;
    void Start()
    {
        alkuRotaatio = transform.rotation;
    }


    void Update()
    {
        
        transform.Rotate(Vector3.forward * -LoruInput.GetAxis("Horizontal")*Time.deltaTime*100);
        if (LoruInput.GetAxis("Horizontal") == 0)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, alkuRotaatio, 0.016f);
        }
        
    }
}

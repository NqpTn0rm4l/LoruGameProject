using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alkuteksti : MonoBehaviour
{
   
    void Update()
    {
        if (Input.GetButtonDown("Jump") || Input.GetMouseButtonDown(0))
        {
            Destroy(gameObject);
        }
    }
}

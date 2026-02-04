using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AikaTuhoaja : MonoBehaviour
{
    public float aika = 1;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("Tuhoa", aika); 
    }

    void Tuhoa()
    {
        Destroy(gameObject);
    } 
}

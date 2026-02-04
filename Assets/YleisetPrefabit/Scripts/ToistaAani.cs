using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToistaAani : MonoBehaviour
{
    private void OnEnable()
    {
        GetComponent<AudioSource>().Play();
    }
}

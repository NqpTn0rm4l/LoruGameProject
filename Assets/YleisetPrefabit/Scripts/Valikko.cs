using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Valikko : MonoBehaviour
{
    public void AloitaPeli()
    {
        SceneManager.LoadScene("Peli");
    }
}

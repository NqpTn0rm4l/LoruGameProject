using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ValikkoScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per framefwef
    void Update()
    {
        
    }
    public void Aloita()
    {
        GameObject.Find("Musiikki").GetComponent<Musiikki>().HiljennaMusa();
        SceneManager.LoadScene(1);
    }
}

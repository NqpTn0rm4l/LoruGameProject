using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AlkuValikko : MonoBehaviour
{
    public string valikkoScenenNimi;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("Vaihda", 3.5f);
    }

  
    void Vaihda()
    {
        
            SceneManager.LoadScene(valikkoScenenNimi);
        
        
    }
}

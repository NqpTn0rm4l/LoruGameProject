using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class PelaaNappi : MonoBehaviour
{
    Button nappi;
    // Start is called before the first frame update
    void Start()
    {
        if (GetComponent<Button>() != null)
        {
            nappi = GetComponent<Button>();
            nappi.onClick.AddListener(Pelaa);
        }
    }

    // Update is called once per frame
    void Update()
    {
#if !UNITY_WEBGL
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
#endif
    }

    private void OnMouseDown()
    {
        Pelaa();
       
    }

   public void Pelaa()
    {
        PeliManageri.SiirryPeliin();
       
    }
}

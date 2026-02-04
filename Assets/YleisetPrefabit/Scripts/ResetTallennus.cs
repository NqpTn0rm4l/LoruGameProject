using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ResetTallennus : MonoBehaviour
{
    Button nappi;
    // Start is called before the first frame update
    void Start()
    {
        nappi = GetComponent<Button>();
        nappi.onClick.AddListener(Resettaa);
    }

    // Update is called once per frame
    void Resettaa()
    {
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene("PommiValikko");
    }
}

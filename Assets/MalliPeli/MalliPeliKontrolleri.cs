using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class MalliPeliKontrolleri : MonoBehaviour
{
    [SerializeField]
    private GameObject[] Cards;
    public bool CanAnswer = true;
    public GameObject[] Hearts;
    // Start is called before the first frame update
    void Start()
    {
        VastausKontrolleri.Instance.UusiTehtava();
        Cards = GameObject.FindGameObjectsWithTag("Card");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void VastaaOikein()
    {
        VastausKontrolleri.Vastaa("Oikein");
        StartCoroutine(NewQuestionDelay());
        //VastausKontrolleri.Instance.UusiTehtava();
    }

    public void VastaaVaarin()
    {
        Debug.Log("Wrong Answer");
        LoseHeart();
        //VastausKontrolleri.Instance.UusiTehtava();
    }

    public static MalliPeliKontrolleri instance;
    void Awake()
    {
        
            if (instance == null)
            {
                instance = this;
            }
            else if (instance != null)
            {
                Destroy(gameObject);
            }
        
    }

    IEnumerator NewQuestionDelay()
    {
        CanAnswer = false;
        yield return new WaitForSeconds(2);
        foreach (GameObject card in Cards)
        {
            card.SetActive(true);
        }
        VastausKontrolleri.Instance.UusiTehtava();
        CanAnswer = true;
    }

    private void LoseHeart()
    {
        /*for (int i = 0; i < Hearts.Length; ++i)
        {
            if (Hearts[10].activeSelf)
            { 

            }
        }*/
        /*Hearts[Hearts.Length - 1].SetActive(false);
        Debug.Log("Heart Lost");*/
        //Hearts[Hearts.Length - 1].SetActive(false);
        Destroy(Hearts[Hearts.Length - 1]);
        Hearts = Hearts.Take(Hearts.Count() - 1).ToArray();
        /*foreach (GameObject heart in Hearts)
        {
            if (heart.activeSelf != true)
            {
                Destroy(heart);
            }
        }*/
    }
}

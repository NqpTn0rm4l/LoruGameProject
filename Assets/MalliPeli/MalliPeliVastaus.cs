using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class MalliPeliVastaus : MonoBehaviour
{
    public MalliPeliKontrolleri MalliPeliKontrolleri;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        /*if (collision.transform.CompareTag("Pallo"))
        {
            if (gameObject.name == "OVastaus")
            {
                MalliPeliKontrolleri.instance.VastaaOikein();
            }
            else
            {
                MalliPeliKontrolleri.instance.VastaaVaarin();
            }
            collision.gameObject.GetComponent<MallipeliPallo>().Palauta();
        }
        */
    }
    private void Start()
    {      
    }
    private void Update()
    {
        //arraygameobject = GameObject.FindGameObjectsWithTag("Card");
        if (Input.GetMouseButtonDown(0) && MalliPeliKontrolleri.CanAnswer == true)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.gameObject.CompareTag("Card"))
                {
                    if (hit.transform.GetChild(0).gameObject.name == "OVastaus")
                    {
                        MalliPeliKontrolleri.instance.VastaaOikein();
                        Debug.Log("Works");
                    }
                    if(hit.transform.GetChild(0).gameObject.name == "VVastausYksi")
                    {
                        Debug.Log("WRONG");
                        hit.transform.gameObject.SetActive(false);
                        MalliPeliKontrolleri.instance.VastaaVaarin();
                    }
                    if(hit.transform.GetChild(0).gameObject.name == "VVastausKaksi")
                    {
                        Debug.Log("WRONGII");
                        hit.transform.gameObject.SetActive(false);
                        MalliPeliKontrolleri.instance.VastaaVaarin();
                    }
                    if(hit.transform.GetChild(0).gameObject.name == "VVastausKolme")
                    {
                        Debug.Log("WRONGIII");
                        hit.transform.gameObject.SetActive(false);
                        MalliPeliKontrolleri.instance.VastaaVaarin();
                    }
                }
            }
        }
    }
}


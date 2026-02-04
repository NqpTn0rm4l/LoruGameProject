using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvaaVaikeustaso : MonoBehaviour
{
   public void AvaaVaikeusTaso()
    {
        if (GameObject.Find("Vaikeustaso") != null)
        {

            GameObject.Find("Vaikeustaso").transform.Find("Vaikeustaso").gameObject.SetActive(true);
        }
        else
        {
       
            Debug.Log("Vaikeustaso Objektia ei löydy");
        }
    }
}

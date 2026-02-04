using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VastausAjastin : MonoBehaviour
{
    

    public static void KutsuVastaus()
    {
        GameObject.Find("Vastausajastin").GetComponent<VastausAjastin>().Temp();
    }

    void Temp()
    {

        Invoke("Vastaus", 1);
    }

   public void Vastaus()
    {
        GameObject.Find("VastausKontrolleri").GetComponent<VastausKontrolleri>().UusiTehtava();
    }
}

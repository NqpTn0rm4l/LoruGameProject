using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseButton : MonoBehaviour
{
    public Rigidbody2D rb;
    public float speeed;
    bool nappiPainettu;
    private void OnMouseDown()
    {
        speeed = rb.linearVelocity.magnitude;
        
        if (!nappiPainettu)
        {
            Time.timeScale = 0;
            nappiPainettu = true;
        }
        else
        {
            rb.linearVelocity = Vector3.zero;
            Time.timeScale = 1;
            nappiPainettu = false;
            
        }
        rb.linearVelocity = Vector3.zero;
    }
}

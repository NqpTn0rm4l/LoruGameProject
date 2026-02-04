using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MallipeliPallo : MonoBehaviour
{
    Vector3 sijaintiAlussa;
    Rigidbody2D rb;


    // Start is called before the first frame update
    void Start()
    {
        sijaintiAlussa = transform.position;
        rb = GetComponent<Rigidbody2D>();
        rb.linearDamping = 4 - VaikeusTaso.taso;
    }

    private void Update()
    {
        if (transform.position.y < -5)
        {
            Palauta();

        }
    }

    // Update is called once per frame
    public void Palauta()
    {
        transform.position = sijaintiAlussa;
        rb.linearVelocity = Vector2.zero;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaustaScrollaaja : MonoBehaviour
{
    public float nopeus;
    GameObject kamera;
    
    // Start is called before the first frame update
    MeshRenderer rend;
    Vector2 alkuOffSet;
    float alkuX;
    void Start()
    {
        rend = GetComponent<MeshRenderer>();
        kamera = Camera.main.gameObject;
        alkuX = transform.position.x;
        alkuOffSet = rend.material.mainTextureOffset;

    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = new Vector3(alkuX, kamera.transform.position.y,transform.position.z);

        rend.material.mainTextureOffset =  new Vector2(0, alkuOffSet.y + transform.position.y * nopeus);
       
    }
}

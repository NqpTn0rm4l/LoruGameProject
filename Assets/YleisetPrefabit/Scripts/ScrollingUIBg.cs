using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollingUIBg : MonoBehaviour
{
    public float speed;
    RawImage bgImage;
    float y;

    // Start is called before the first frame update
    void Start()
    {
        bgImage = GetComponent<RawImage>();
    }

    // Update is called once per frame
    void Update()
    {
        y += speed * Time.deltaTime * speed ;
        bgImage.uvRect = new Rect(0, -y/100, 1, 1);
    }
}

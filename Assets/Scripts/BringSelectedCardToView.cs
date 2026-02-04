using System.Threading;
using UnityEngine;

public class BringSelectedCardToView : MonoBehaviour
{
    private GameObject[] Cards;
    Vector3 ViewPosition = new Vector3(0f, 7f, -7f);
    Vector3 NormalPosition;
    Quaternion ViewRotation = Quaternion.Euler(-22f, 0, 0);
    Quaternion NormalRotation = Quaternion.Euler(0f, 0f, 0f);
    public float timer = 0.0f;
    public bool mouseIsHoveringOver;
    
    void Start()
    {
        NormalPosition = transform.position;
        NormalRotation = transform.rotation;
        mouseIsHoveringOver = false;
        Cards = GameObject.FindGameObjectsWithTag("Card");
    }
    /* void Update()
     {
         if (Input.GetMouseButtonDown(0))
         {
             Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
             RaycastHit hit;
             if (Physics.Raycast(ray, out hit) && hit.transform.CompareTag(("Card")))
             {
                 hit.collider.gameObject.transform.position = ViewPosition;
             }
         }
     }
    */
    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            timer = 0.0f;
            mouseIsHoveringOver = false;
            gameObject.transform.position = NormalPosition;
            gameObject.transform.rotation = NormalRotation;
        }
    }
    private void OnMouseOver()
    {
        timer += Time.deltaTime;
        if(timer >= 3.0f)
        {
            transform.position = ViewPosition;
            transform.rotation = ViewRotation;
        }
        mouseIsHoveringOver = true;
    }
    /*private void OnMouseClick()
    {
        timer = 0.0f;
        mouseIsHoveringOver = false;
    }*/
}

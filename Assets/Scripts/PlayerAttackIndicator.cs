using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerAttackIndicator : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public bool _isDragging;

    public GameObject _Player;

    public void Start()
    {
        _Player = GameObject.FindGameObjectWithTag("Player");
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        _Player.GetComponent<LineRenderer>().SetPosition(0, _Player.transform.position);
    }

    public void OnDrag(PointerEventData eventData)
    {

    }

    public void OnEndDrag(PointerEventData eventData)
    {

    }
}

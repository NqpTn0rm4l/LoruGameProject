using System.Collections;
using System.Threading;
using UnityEngine;

public class BringSelectedCardToView : MonoBehaviour
{
    private GameObject[] _Cards;
    private GameObject _CardInView;
    Vector3 _NormalPosition;
    Vector3 _CurrentPosition;
    Vector3 _ViewPosition = new Vector3(0f, 7f, -4f);
    Quaternion _NormalRotation;
    Quaternion _ViewRotation = Quaternion.Euler(0f, 0f, 0);
    float _UniversalTimer;
    float _CurrentTransitionToViewTime;
    float _MaxTransitionToViewTime;
    float _CurrentTransitionToNormalTime;
    float _MaxTransitionToNormalTime;
    int _StartPositionX;
    int _StartPositionY;
    int _StartPositionZ;
    int _CurrentPositionXToInt;
    int _CurrentPositionYToInt;
    int _CurrentPositionZToInt;
    bool _CanCount;
    bool _CanCount2;


    void Start()
    {
        _Cards = GameObject.FindGameObjectsWithTag("Card");
        _NormalPosition = transform.position;
        _NormalRotation = transform.rotation;
        _CurrentTransitionToViewTime = 0f;
        _MaxTransitionToViewTime = 2.0f;
        _CurrentTransitionToNormalTime = 0f;
        _MaxTransitionToNormalTime = 2.0f;
        _StartPositionX = (int)_NormalPosition.x;
        _StartPositionY = (int)_NormalPosition.y;
        _StartPositionZ = (int)_NormalPosition.z;
        _CanCount = true;
        _CanCount2 = !_CanCount2;
    }

    void Update()
    {
        _CurrentPosition = transform.position;
        _CurrentPositionXToInt = (int)_CurrentPosition.x;
        _CurrentPositionYToInt = (int)_CurrentPosition.y;
        _CurrentPositionZToInt = (int)_CurrentPosition.z;

        if (Input.GetMouseButtonDown(0))
        {
            _UniversalTimer = 0.0f;
            GameObject.FindGameObjectWithTag("CardInView").transform.position = Vector3.Lerp(_ViewPosition, _NormalPosition, _CurrentTransitionToNormalTime / _MaxTransitionToNormalTime);
            GameObject.FindGameObjectWithTag("CardInView").transform.rotation = Quaternion.Lerp(_ViewRotation, _NormalRotation, _CurrentTransitionToNormalTime / _MaxTransitionToNormalTime);
        }
    }

    private void OnMouseOver()
    {
        _CanCount = true;
        _UniversalTimerCount();
        float _ActivationTime = 1.5f;
        if (_UniversalTimer >= _ActivationTime)
        {
            gameObject.tag = "CardInView";
            _CanCount = !_CanCount;
            _CanCount2 = true;
            _TransitionToViewTimerCount();
            transform.position = Vector3.Lerp(_NormalPosition, _ViewPosition, _CurrentTransitionToViewTime / _MaxTransitionToViewTime);
            transform.rotation = Quaternion.Lerp(_NormalRotation, _ViewRotation, _CurrentTransitionToViewTime / _MaxTransitionToViewTime);
            if(_CurrentPositionXToInt == _StartPositionX && _CurrentPositionYToInt ==  _StartPositionY && _CurrentPositionZToInt == _StartPositionZ)
            {
                _CanCount2 = !_CanCount2;
            }
        }
    }

    private void OnMouseExit()
    {
        _CanCount = !_CanCount;
        _UniversalTimer = 0.0f;
    }

    void _UniversalTimerCount()
    {
        if(_CanCount)
        {
            _UniversalTimer += Time.deltaTime;
        }
    }

    void _TransitionToViewTimerCount()
    {
        if(_CanCount2)
        {
            _CurrentTransitionToViewTime += Time.deltaTime;
        }
    }
}

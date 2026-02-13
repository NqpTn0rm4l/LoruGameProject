using System.Collections;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;

public class BringSelectedCardToView : MonoBehaviour
{
    private GameObject[] _Cards;
    public GameObject _CardInView;
    Vector3 _NormalPosition;
    public Vector3 _CurrentPosition;
    Vector3 _ViewPosition = new Vector3(0f, 7f, -4f);
    Quaternion _NormalRotation;
    Quaternion _ViewRotation = Quaternion.Euler(0f, 0f, 0);
    public float _UniversalTimer;
    public float _CurrentTransitionToViewTime;
    public float _MaxTransitionToViewTime;
    public float _CurrentTransitionToNormalTime;
    public float _MaxTransitionToNormalTime;
    int _StartPositionX;
    int _StartPositionY;
    int _StartPositionZ;
    int _CurrentPositionXToInt;
    int _CurrentPositionYToInt;
    int _CurrentPositionZToInt;
    public bool _CanCount;
    public bool _CanCount2;


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
        _CanCount2 = false;
    }

    public void Update()
    {
        _CurrentPosition = transform.position;
        _CurrentPositionXToInt = (int)_CurrentPosition.x;
        _CurrentPositionYToInt = (int)_CurrentPosition.y;
        _CurrentPositionZToInt = (int)_CurrentPosition.z;
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log(_CardInView);
            _UniversalTimer = 0.0f;

            if(_CardInView != null ) 
            {
                StartCoroutine(ReturnToNormal());
            }
            else
            {
                return;
            }
        }
    }

    public void OnMouseOver()
    {
        _CanCount = true;
        _UniversalTimerCount();
        float _ActivationTime = 1.5f;
        if (_UniversalTimer >= _ActivationTime)
        {
            gameObject.tag = "CardInView";
            StartCoroutine(LookForCardInView());
            _CanCount = !_CanCount;
            _CanCount2 = true;
            _TransitionToViewTimerCount();
            transform.position = Vector3.Lerp(_NormalPosition, _ViewPosition, _CurrentTransitionToViewTime / _MaxTransitionToViewTime);
            transform.rotation = Quaternion.Lerp(_NormalRotation, _ViewRotation, _CurrentTransitionToViewTime / _MaxTransitionToViewTime);
            if(_CurrentPositionXToInt == _StartPositionX && _CurrentPositionYToInt ==  _StartPositionY && _CurrentPositionZToInt == _StartPositionZ)
            {
                _CanCount2 = !_CanCount2;
                _CurrentTransitionToViewTime = 0.0f;
            }
        }
    }

    public void OnMouseExit()
    {
        _CanCount = !_CanCount;
        _UniversalTimer = 0.0f;
        StartCoroutine(LookForCardInView());
        if(_CardInView != null)
        {
            StartCoroutine(ReturnToNormal());
        }
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

    IEnumerator LookForCardInView()
    {
        _CardInView = GameObject.FindGameObjectWithTag("CardInView");
        yield return null;
    }

    public IEnumerator ReturnToNormal()
    {
        while (_CurrentTransitionToNormalTime > _MaxTransitionToNormalTime)
        {
            _CurrentTransitionToViewTime += Time.deltaTime;
            float _Sum = _CurrentTransitionToViewTime / _CurrentTransitionToNormalTime;
            _CardInView.transform.position = Vector3.Lerp(_ViewPosition, _NormalPosition, _Sum);
            _CardInView.transform.rotation = Quaternion.Lerp(_ViewRotation, _NormalRotation, _Sum);
            yield return null;
        }
        _CardInView.tag = "Card";
    }

    /*public IEnumerator ReturnToNormalII()
    {
        _CardInView.transform.position = Vector3.Lerp(_CurrentPosition, _NormalPosition, _CurrentTransitionToNormalTime / _MaxTransitionToNormalTime);
        _CardInView.transform.rotation = Quaternion.Lerp(_ViewRotation, _NormalRotation, _CurrentTransitionToNormalTime / _MaxTransitionToNormalTime);
        yield return null;
    }*/
}

/*using System.Collections;
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
            _CardInView = GameObject.FindGameObjectWithTag("CardInView");
            StartCoroutine(ReturnToNormal());
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

    IEnumerator ReturnToNormal()
    {
        _CardInView.transform.position = Vector3.Lerp(_ViewPosition, _NormalPosition, _CurrentTransitionToNormalTime / _MaxTransitionToNormalTime);
        _CardInView.transform.rotation = Quaternion.Lerp(_ViewRotation, _NormalRotation, _CurrentTransitionToNormalTime / _MaxTransitionToNormalTime);
    }
}

*/
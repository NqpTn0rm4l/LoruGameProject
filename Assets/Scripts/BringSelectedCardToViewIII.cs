using System.Collections;
using UnityEngine;

public class BringSelectedCardToViewIII : MonoBehaviour
{
    private static GameObject _SelectedCard;

    private Vector3 _NormalPosition;
    private Quaternion _NormalRotation;

    [SerializeField] private Vector3 _ViewPosition;
    [SerializeField] private Quaternion _ViewRotation;

    [SerializeField] private float _Timer = 0;
    [SerializeField] private float _MaxTimeToFinish = 2.0f;

    private void Start()
    {
        _NormalPosition = transform.position;
        _NormalRotation = transform.rotation;
        _ViewPosition = new Vector3(0, 7, -4);
        _ViewRotation = Quaternion.Euler(0, 0, 0);
    }

    private void OnMouseDown()
    {
        if ( _SelectedCard == null )
        {
            _SelectedCard = gameObject;
            Debug.Log(_SelectedCard.ToString());
            StartCoroutine(_BringToView());
        }

        else if ( _SelectedCard == gameObject )
        {
            StartCoroutine(_ReturnToTable());
        }
    }

    private IEnumerator _BringToView()
    {
        while ( _Timer < _MaxTimeToFinish)
        {
            _Timer += Time.deltaTime;
            _SelectedCard.transform.position = Vector3.Lerp( _NormalPosition,   _ViewPosition, _Timer / _MaxTimeToFinish );
            _SelectedCard.transform.rotation = Quaternion.Lerp( _NormalRotation, _ViewRotation, _Timer / _MaxTimeToFinish) ;
            yield return null;
        }
        _Timer = 0.0f;
        yield return null;
    }

    private IEnumerator _ReturnToTable()
    {
        while ( _Timer < _MaxTimeToFinish )
        {
            _Timer += Time.deltaTime;
            _SelectedCard.transform.position = Vector3.Lerp( _ViewPosition, _NormalPosition, _Timer / _MaxTimeToFinish );
            _SelectedCard.transform.rotation = Quaternion.Lerp(_ViewRotation, _NormalRotation, _Timer / _MaxTimeToFinish);
            yield return null;  
        }
        _Timer = 0.0f;
        _SelectedCard = null;
        yield return null;
    }
}

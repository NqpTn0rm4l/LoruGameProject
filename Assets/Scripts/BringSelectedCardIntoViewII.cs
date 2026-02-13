using System.Collections;
using Unity.Mathematics;
using UnityEngine;

public class BringSelectedCardIntoViewII : MonoBehaviour
{
    private static BringSelectedCardIntoViewII _CardInView;

    private Vector3 _NormalPosition;
    private quaternion _NormalRotation;

    [SerializeField] private Vector3 _ViewPosition;
    [SerializeField] private Quaternion _ViewRotation;

    [SerializeField] private float _TransitionDuration;

    private Coroutine _RunningCoroutine;
    private bool _IsInView = false;

    private void Start()
    {
        _NormalPosition = transform.position;
        _NormalRotation = transform.rotation;
    }

    private void OnMouseDown()
    {
        if( _CardInView != null && !this )
        {
            _CardInView._ReturnToNormal();
        }

        if( _IsInView)
        {
            _MoveToView();
        }
        else
        {
            _ReturnToNormal();
        }
    }

    private void _MoveToView()
    {
        _IsInView = true;
        _CardInView = this;

        _StartTransition( _ViewPosition, _ViewRotation );
    }

    private void _ReturnToNormal()
    {
        _IsInView = false;

        if( _CardInView == this)
        {
            _CardInView = null;
        }

        _StartTransition( _ViewPosition, _ViewRotation );
    }

    private void _StartTransition( Vector3 _TargetPosition, Quaternion _TargetRotation )
    {

        if( _RunningCoroutine != null )
        {
            StopCoroutine(_RunningCoroutine); 
        }

        _RunningCoroutine = StartCoroutine(AnimateTransition(_TargetPosition, _TargetRotation));
    }

    private IEnumerator AnimateTransition(Vector3 _TargetPosition, Quaternion _TargetRotation )
    {
        Vector3 _StartPosition = transform.position;
        Quaternion _StartRotation = transform.rotation;

        float _Timer = 0.0f;

        while ( _Timer < _TransitionDuration )
        {
            _Timer += Time.deltaTime;
            float _Duration = _Timer / _TransitionDuration;

            _Timer = _Timer * _Timer * ( 3.0f - 2.0f * _Timer );

            transform.position = Vector3.Lerp(_StartPosition, _TargetPosition, _Duration );
            transform.rotation = Quaternion.Lerp(_StartRotation, _TargetRotation, _Duration );

            yield return null;
        }

        transform.position = _TargetPosition;
        transform.rotation = _TargetRotation;
    }
}

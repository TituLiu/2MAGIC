﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoyStick : MonoBehaviour
{
    public float radius;

#region EVENTS
    public delegate void DragStick(float x);
    public event DragStick OnDragStick;

    public delegate void EndStick(float x);
    public event EndStick OnEndDragStick;
#endregion
    Vector3 _originalPosition;
    [SerializeField] Vector3 _stickValue;
    Vector3 _stickPos;

    Coroutine _CoroutineDrag;

    private void Start()
    {
        _originalPosition = transform.position;
    }

    public void OnDrag()
    {
        if (_CoroutineDrag != null) StopCoroutine(_CoroutineDrag);

        _CoroutineDrag = StartCoroutine(ArtificialUpdateDrag());
    }

    IEnumerator ArtificialUpdateDrag()
    {
        var WaitForEndOfFrame = new WaitForEndOfFrame();
        while (true)
        {
            _stickPos = Vector3.ClampMagnitude(Input.mousePosition - _originalPosition, radius);
            _stickValue = Vector3.ClampMagnitude(Input.mousePosition - _originalPosition, radius);
            _stickPos.y = 0;
            transform.position = _stickPos + _originalPosition;           
            if (OnDragStick != null) OnDragStick(_stickValue.x);
            yield return WaitForEndOfFrame;
        }
    }

    public void OnEndDrag()
    {
        if (_CoroutineDrag != null)
        {
            StopCoroutine(_CoroutineDrag);
            _CoroutineDrag = null;
        }

        transform.position = _originalPosition;
        _stickValue = Vector3.zero;

        if (OnEndDragStick != null) OnEndDragStick(_stickValue.x);
    }
}


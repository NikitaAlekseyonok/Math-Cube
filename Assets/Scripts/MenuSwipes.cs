// Copyright (c) 2012-2019 FuryLion Group. All Rights Reserved.

using UnityEngine;

public class MenuSwipes : MonoBehaviour
{
    private bool _isDragging;
    private Vector2 _tapPoint;
    private Vector2 _swipeDelta;
    private const float _minSwipeDelta = 130;

    public enum SwipeType
    {
        Left,
        Right
    }

    public delegate void OnSwipeInput(SwipeType type);
    public static event OnSwipeInput SwipeEvent;

    private void Update()
    {
        if (!Application.isMobilePlatform)
        {
            if (Input.GetMouseButtonDown(0))
            {
                _isDragging = true;
                _tapPoint = Input.mousePosition;
            }
            else if (Input.GetMouseButtonUp(0))
                ResetSwipe();
        }
        else
        {
            if (Input.touchCount > 0)
            {
                if (Input.GetTouch(0).phase == TouchPhase.Began)
                {
                    _isDragging = true;
                    _tapPoint = Input.touches[0].position;
                }
                else if (Input.GetTouch(0).phase == TouchPhase.Canceled ||
                         Input.GetTouch(0).phase == TouchPhase.Ended)
                    ResetSwipe();
            }
        }

        CalculateSwipe();
    }

    private void CalculateSwipe()
    {
        _swipeDelta = Vector2.zero;

        if (_isDragging)
        {
            if (!Application.isMobilePlatform && Input.GetMouseButton(0))
                _swipeDelta = (Vector2)Input.mousePosition - _tapPoint;
            else if (Input.touchCount > 0)
                _swipeDelta = Input.touches[0].position - _tapPoint;
        }

        if (_swipeDelta.magnitude <= _minSwipeDelta)
            return;

        if (SwipeEvent != null)
        {
            if (Mathf.Abs(_swipeDelta.x) > Mathf.Abs(_swipeDelta.y))
                SwipeEvent(_swipeDelta.x < 0 ? SwipeType.Left : SwipeType.Right);
        }

        ResetSwipe();
    }

    private void ResetSwipe()
    {
        _isDragging = false;
        _tapPoint = _swipeDelta = Vector2.zero;
    }
}

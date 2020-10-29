// Copyright (c) 2012-2019 FuryLion Group. All Rights Reserved.

using DG.Tweening;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static int PlayerRow;
    public static int PlayerCol;
    public static Transform Transform;

    public float RotationPeriod = 0.3f;
    public float SideLength = 2f;

    private bool _isRotate;
    private float _directionX;              
    private float _directionZ;              

    private Vector3 _startPos;
    private float _rotationTime;     
    private float _radius;

    public Quaternion FromRotation;
    public Quaternion ToRotation;
    public AudioSource MoveSound;

    public void Start()
    {
        PlayerRow = 1;
        PlayerCol = 1;
        _radius = SideLength * Mathf.Sqrt(2f) / 2f;
        Swipes.SwipeEvent += Input;
        Transform = GetComponent<Transform>();
    }

    public void FixedUpdate()
    {
        if (!_isRotate)
            return;

        MoveSound.Play();
        _rotationTime += Time.fixedDeltaTime;
        var ratio = Mathf.Lerp(0, 1, _rotationTime / RotationPeriod);
        var thetaRad = Mathf.Lerp(0, Mathf.PI / 2f, ratio);
        var distanceX = DistanceFormula(-_directionX, thetaRad);
        var distanceY = _radius * (Mathf.Sin(45f * Mathf.Deg2Rad + thetaRad) - Mathf.Sin(45f * Mathf.Deg2Rad));
        var distanceZ = DistanceFormula(_directionZ, thetaRad);
        transform.position = new Vector3(_startPos.x + distanceX, _startPos.y + distanceY, _startPos.z + distanceZ);
        transform.rotation = Quaternion.Lerp(FromRotation, ToRotation, ratio);

        if (Mathf.Approximately(ratio, 1f))
        {
            _isRotate = false;
            _directionX = 0;
            _directionZ = 0;
            _rotationTime = 0;
        }
    }

    private float DistanceFormula(float direction, float thetaRad)
    {
        return direction * _radius * (Mathf.Cos(45f * Mathf.Deg2Rad) - Mathf.Cos(45f * Mathf.Deg2Rad + thetaRad));
    }

    private void Input(Swipes.SwipeType type)
    {
        if (!GameManager.Move)
            return;

        if (type == Swipes.SwipeType.Down && PlayerRow != 2)
        {
            _directionX = 0;
            _directionZ = -1;
            PlayerRow += 1;
        }
        else if (type == Swipes.SwipeType.Up && PlayerRow != 0)
        {
            _directionX = 0;
            _directionZ = 1;
            PlayerRow -= 1;
        }
        else if (type == Swipes.SwipeType.Left && PlayerCol != 0)
        {
            _directionX = 1;
            _directionZ = 0;
            PlayerCol -= 1;
        }
        else if (type == Swipes.SwipeType.Right && PlayerCol != 2)
        {
            _directionX = -1;
            _directionZ = 0;
            PlayerCol += 1;
        }

        _startPos = transform.position;
        FromRotation = transform.rotation;
        transform.Rotate(_directionZ * 90, 0, _directionX * 90, Space.World);
        ToRotation = transform.rotation;
        transform.rotation = FromRotation;
        _rotationTime = 0;
        _isRotate = true;
    }

    public static void BLevel()
    {
        Transform.DOMove(new Vector3(GameManager.BeatenPanelPos.x, Transform.position.y, GameManager.BeatenPanelPos.z), 2f).OnComplete(() =>
        {
            PlayerRow = GameManager.OldRow;
            PlayerCol = GameManager.OldCol;
        });
    }

    public void OnDisable()
    {
        Swipes.SwipeEvent -= Input;
    }
}


// Copyright (c) 2012-2019 FuryLion Group. All Rights Reserved.

using UnityEngine;
using DG.Tweening;

public class SkinMove : MonoBehaviour
{
    public void Start()
    {
        transform.DOMoveY(1, 1.5f).SetLoops(-1, LoopType.Yoyo);
    }

    public void Update()
    {
        transform.Rotate(Vector3.up, 1f);
    }
}

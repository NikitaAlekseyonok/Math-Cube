// Copyright (c) 2012-2019 FuryLion Group. All Rights Reserved.

using UnityEngine;
using DG.Tweening;

public class TrainingAnimation : MonoBehaviour
{
    public GameObject ArrowTop;
    public GameObject ArrowBot;
    public GameObject ArrowLeft;
    public GameObject ArrowRight;
    public GameObject ArrowText;

    private void Start()
    {
        ArrowTop.transform.DOScaleY(1.5f, 1f).SetLoops(-1, LoopType.Yoyo);
        ArrowTop.transform.DOScaleX(0.8f, 1f).SetLoops(-1, LoopType.Yoyo);
        ArrowBot.transform.DOScaleY(1.5f, 1f).SetLoops(-1, LoopType.Yoyo);
        ArrowBot.transform.DOScaleX(0.8f, 1f).SetLoops(-1, LoopType.Yoyo);
        ArrowLeft.transform.DOScaleX(1.5f, 1f).SetLoops(-1, LoopType.Yoyo);
        ArrowLeft.transform.DOScaleY(0.8f, 1f).SetLoops(-1, LoopType.Yoyo);
        ArrowRight.transform.DOScaleX(1.5f, 1f).SetLoops(-1, LoopType.Yoyo);
        ArrowRight.transform.DOScaleY(0.8f, 1f).SetLoops(-1, LoopType.Yoyo);
        ArrowText.transform.DOScale(1.5f, 2f).SetLoops(-1, LoopType.Yoyo);
    }
}

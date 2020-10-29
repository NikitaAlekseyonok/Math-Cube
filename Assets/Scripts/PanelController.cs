// Copyright (c) 2012-2019 FuryLion Group. All Rights Reserved.

using UnityEngine;
using DG.Tweening;

public class PanelController : MonoBehaviour
{
    public int Row;
    public int Col;
    private TextMesh _panelResult;

    public void Start()
    {
        _panelResult = GetComponentInChildren<TextMesh>();
    }

    public void Update()
    {
        if (Row == PlayerController.PlayerRow && Col == PlayerController.PlayerCol)
            _panelResult.gameObject.SetActive(false);
        else
            _panelResult.gameObject.SetActive(true);

        if (Row == GameManager.NewRow && Col == GameManager.NewCol)
        {
            Row = GameManager.OldRow;
            Col = GameManager.OldCol;
            GameManager.OldRow = GameManager.NewRow;
            GameManager.OldCol = GameManager.NewCol;
            transform.localPosition = new Vector3(18.36f + (Col * 2.75f), transform.localPosition.y, 2.56f - (Row * 2.75f));
        }

        if (GameManager.StartGame)
        {
            int result;

            do
            {
                result = Random.Range(1, 100);
                _panelResult.text = result.ToString();
            }
            while (result == GameManager.Result);

            GameManager.CountPanels++;

            if (GameManager.CountPanels == 8)
                GameManager.StartGame = false;

        }
    }
}

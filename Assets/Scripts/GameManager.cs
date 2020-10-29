// Copyright (c) 2012-2019 FuryLion Group. All Rights Reserved.

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using DG.Tweening;

public class GameManager : MonoBehaviour
{
    public static int Result;
    public static int OldRow;
    public static int OldCol;
    public static int NewRow;
    public static int NewCol;
    public static int CountPanels;
    public static int Score;
    public static int Coins;
    public static int Bonus;

    public static bool StartGame;
    public static bool Move;
    public static Vector3 BeatenPanelPos;
    private static Vector3 _bonusSpawn = new Vector3(21.11f, 19.19f, -0.19f);
    private static Vector3[] CoinSpawn = { new Vector3(18.36f, 19.19f, 2.56f),
                                           new Vector3(21.11f, 19.19f, 2.56f),
                                           new Vector3(23.86f, 19.19f, 2.56f),
                                           new Vector3(18.36f, 19.19f, -0.19f),
                                           new Vector3(21.11f, 19.19f, -0.19f),
                                           new Vector3(23.86f, 19.19f, -0.19f),
                                           new Vector3(18.36f, 19.19f, -2.94f),
                                           new Vector3(21.11f, 19.19f, -2.94f),
                                           new Vector3(23.86f, 19.19f, -2.94f)};

    public GameObject MainPanel; 
    public GameObject BeatenPanel;
    public GameObject DownPanel;
    public GameObject LeftPanel;
    public GameObject TextCondition;

    public GameObject BonusCoin;
    public GameObject BonusTime;
    public GameObject BonusLevel;

    public GameObject PlayButton;
    public GameObject PauseButton;

    private TextMesh _scoreText;
    private TextMesh _condition;
    private TextMesh _beatenPanelResult;
    public Text TextCoin;

    public bool EndAnim = true;
    public bool Loose;
    private bool _pause;

    private int _number1;
    private int _number2;
    private int _numOperation;
    private char[] _operations = {'+', '-', '*'};

    private IEnumerator Start()
    {
        Coins = 0;
        Score = -1;
        OldRow = 1;
        OldCol = 1;
        NewRow = -1;
        NewCol = -1;
        _scoreText = LeftPanel.GetComponentInChildren<TextMesh>();
        _condition = TextCondition.GetComponent<TextMesh>();
        _beatenPanelResult = BeatenPanel.GetComponentInChildren<TextMesh>();
        MainPanel.GetComponentInChildren<TextMesh>().text = Menu.Save.HScore.ToString();
        Invoke("NewGame", 3);
        while (true)
        {
            yield return new WaitForSeconds(15.0f);

            if(!Swipes.SetTraining)
                CreateBonus();
        }
    }

    private void ScaleBack() 
    {
        DownPanel.transform.DOScale(new Vector3(10f, 1, 10f), 1f).OnComplete(() => {
            EndAnim = true;
            Move = true;

            if (BonusMovement.TakeBonus == false)
                NewGame();
            else
            {
                Scale();
                BonusMovement.TakeBonus = false;
            }
        });
    }

    private void NewGame()
    {
        if (PlayerController.PlayerRow == OldRow && PlayerController.PlayerCol == OldCol && EndAnim)
        {
            ResGame();

            if (BonusMovement.TakeBonus == false)
            {
                do
                    NewRow = Random.Range(0, 3);
                while (NewRow == OldRow);

                do
                    NewCol = Random.Range(0, 3);
                while (NewCol == OldCol);

                BeatenPanel.transform.localPosition = new Vector3
                {
                    x = 18.36f + NewCol * 2.75f,
                    y = BeatenPanel.transform.localPosition.y,
                     z = 2.56f - NewRow * 2.75f
                };
                _number1 = Random.Range(1, 10);
                _number2 = Random.Range(1, 10);
                _numOperation = Random.Range(0, 3);

                switch (_numOperation)
                {
                    case 0:
                         Result = _number1 + _number2;
                         break;
                    case 1:
                        Result = _number1 - _number2;
                        break;
                    case 2:
                        Result = _number1 * _number2;
                        break;
                }

                _beatenPanelResult.text = Result.ToString();
                _condition.text = $"{_number1}{_operations[_numOperation]}{_number2}";
                BeatenPanelPos = BeatenPanel.transform.position;
            }

            if (Swipes.SetTraining)
                return;

            Scale();
        }
    }

    private void Scale()
    {
      
        DownPanel.transform.DOScale(new Vector3(0.5f, 1, 0.5f), 5f)
            .OnComplete(() => {
            if (Menu.Save.HScore < Score)
                Menu.Save.HScore = Score;

            Loose = true;
        });
    }

    public void ResGame() 
    {
        if(Score>0)
            DOTween.KillAll();

        Score++;
        Coins++;
        Menu.Save.Coins++; 
        _scoreText.text = Score.ToString();
        CountPanels = 0;
        StartGame = true;
        EndAnim = false;

        if (Score % 7 == 0 && Score != 0)
        {
            var i = Random.Range(0, 9);
            Instantiate(BonusCoin, CoinSpawn[i], Quaternion.identity);
        }
    }

    private void Update()
    {
        if (OldRow == PlayerController.PlayerRow && OldCol == PlayerController.PlayerCol)
            _beatenPanelResult.gameObject.SetActive(false);
        else
            _beatenPanelResult.gameObject.SetActive(true);

        if (BonusMovement.TakeBonus)
        {
            if (Bonus == 1)
            {
                DOTween.KillAll();
                Move = false;
                ScaleBack();
            }
            else if (Bonus == 2)
            {
                DOTween.KillAll();
                BonusMovement.TakeBonus = false;
                PlayerController.BLevel(); 
            }

            Bonus = 0;
        }

        if (Loose)
        {
            SceneManager.LoadScene("Menu");
            PlayerPrefs.SetString("Save", JsonUtility.ToJson(Menu.Save));
        }

        if (PlayerController.PlayerRow == OldRow && PlayerController.PlayerCol == OldCol)
        {
            Move = false;
            ScaleBack();
        }

        TextCoin.text = Coins == -1 ? "0" : Coins.ToString(); // как такое возможно?  На старте игры. 
    }

    private void CreateBonus()
    {
        Bonus = Random.Range(1, 3);

        if (Bonus == 1)
            Instantiate(BonusTime, _bonusSpawn, Quaternion.identity);
        else
            Instantiate(BonusLevel, _bonusSpawn, Quaternion.identity);
    }

    public void Pause()
    {
        if (!_pause)
        {
            PauseButton.SetActive(false);
            PlayButton.SetActive(true);
            Time.timeScale = 0;
        }
        if (_pause)
        {
            PauseButton.SetActive(true);
            PlayButton.SetActive(false);
            Time.timeScale = 1;
        }

        _pause = !_pause;
    }
}

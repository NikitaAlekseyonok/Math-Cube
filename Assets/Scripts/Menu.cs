// Copyright (c) 2012-2019 FuryLion Group. All Rights Reserved.

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public static int HScore;
    public static int Coins;
    public static UserProgress Save = new UserProgress(); 

    public Text LastScore;
    public Text TextCoins;
    public Text TextHScore;
    public GameObject NewRecord;

    public void Start()
    {
        if (PlayerPrefs.HasKey("Save"))
        {
            Save = JsonUtility.FromJson<UserProgress>(PlayerPrefs.GetString("Save"));
            HScore = Save.HScore;
            Coins = Save.Coins;
            SkinsCarusel.SelectSkin = Save.SelectSkin;
            SkinsCarusel.OpenSkin2 = Save.OpenSkin2;
            SkinsCarusel.OpenSkin3 = Save.OpenSkin3;
            SkinsCarusel.OpenSkin4 = Save.OpenSkin4;
            SkinsCarusel.OpenSkin5 = Save.OpenSkin5;
            SkinsCarusel.OpenSkin6 = Save.OpenSkin6;
        }

        if (GameManager.Score == HScore && HScore != 0)
            NewRecord.SetActive(true);

        if (SkinsCarusel.SelectSkin == 0)
            SkinsCarusel.SelectSkin = 1;

        LastScore.text = GameManager.Score.ToString();
        TextCoins.text = Coins.ToString();
        TextHScore.text =$"Record: {HScore}";
    }

    public void Play()
    {
        SceneManager.LoadScene("Game");
        PlayerPrefs.SetString("Save", JsonUtility.ToJson(Save));
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.SetString("Save", JsonUtility.ToJson(Save));
    }

}

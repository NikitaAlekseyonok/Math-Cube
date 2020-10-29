// Copyright (c) 2012-2019 FuryLion Group. All Rights Reserved.

using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class SkinsCarusel : MonoBehaviour
{
    public static int SelectSkin = 1;
    public static bool OpenSkin2;
    public static bool OpenSkin3;
    public static bool OpenSkin4;
    public static bool OpenSkin5;
    public static bool OpenSkin6;

    public GameObject[] Skins;
    public Button ButtonSelect;
    private Text _buttonText;

    private bool _endAnim = true;
    private bool _start;
    private float _caruselPosition;

    public void Start()
    {
        MenuSwipes.SwipeEvent += Input;
        _buttonText = ButtonSelect.GetComponentInChildren<Text>();
         Skins = GameObject.FindGameObjectsWithTag("Skin");
     }

    private void Input(MenuSwipes.SwipeType type)
    {
        if (!_endAnim)
            return;

        if (type == MenuSwipes.SwipeType.Left && transform.localPosition.x != -30f)
        {
            _endAnim = false;
            transform.DOMoveX(transform.localPosition.x - 6, 1.5f).OnComplete(() => _endAnim = true);
        }

        if (type == MenuSwipes.SwipeType.Right && transform.localPosition.x != 0)
        {
            _endAnim = false;
            transform.DOMoveX(transform.localPosition.x + 6, 1.5f).OnComplete(() => _endAnim = true);
        }
    }

    public void BuyOrSelect()
    {
        switch (_caruselPosition) // плохо!   А как исправить?
        {
            case 0:
                {
                    SelectSkin = 1;
                    break;
                }
            case -6:
                {
                    if (!OpenSkin2 && Menu.Coins >= 100)
                    {
                        OpenSkin2 = true;
                        Menu.Coins -= 100;
                        Menu.Save.OpenSkin2 = true;
                    }
                    else if (OpenSkin2)
                        SelectSkin = 2;

                    break;
                }
            case -12:
                {
                    if (!OpenSkin3 && Menu.Coins >= 200)
                    {
                        OpenSkin3 = true;
                        Menu.Coins -= 200;
                        Menu.Save.OpenSkin3 = true;
                    }
                    else if (OpenSkin3)
                        SelectSkin = 3;

                    break;
                }
            case -18:
                {
                    if (!OpenSkin4 && Menu.Coins >= 400)
                    {
                        OpenSkin4 = true;
                        Menu.Coins -= 400;
                        Menu.Save.OpenSkin4 = true;
                    }
                    else if (OpenSkin4)
                        SelectSkin = 4;

                    break;
                }
            case -24:
                {
                    if (!OpenSkin5 && Menu.Coins >= 800)
                    {
                        OpenSkin5 = true;
                        Menu.Coins -= 800;
                        Menu.Save.OpenSkin5 = true;
                    }
                    else if (OpenSkin5)
                        SelectSkin = 5;

                    break;
                }
            case -30:
                {
                    if (!OpenSkin6 && Menu.Coins >= 1600)
                    {
                        OpenSkin6 = true;
                        Menu.Save.OpenSkin6 = true;
                        Menu.Coins -= 1600;
                    }
                    else if (OpenSkin6)
                        SelectSkin = 6;

                    break;
                }
        }

        Menu.Save.SelectSkin = SelectSkin;
        PlayerPrefs.SetString("Save", JsonUtility.ToJson(Menu.Save));
        Menu.Save.Coins = Menu.Coins;
    }

    public void Update()
    {
        _caruselPosition = transform.localPosition.x;

        if (SelectSkin != 0 && !_start)
            FirstCaruselMove();

        _start = true;

        switch (_caruselPosition)
        {
            case 0:
                {
                    if (SelectSkin == 1)
                    {
                        _buttonText.text = "Selected";
                        ButtonSelect.interactable = false;
                    }
                    else
                    {
                        _buttonText.text = "Select";
                        ButtonSelect.interactable = true;
                    }

                    break;
                }
            case -6:
                {
                    if (!OpenSkin2)
                    {
                        _buttonText.text = "Buy for 100";
                        ButtonSelect.interactable = Menu.Coins >= 100;
                    }
                    else if (SelectSkin == 2)
                    {
                        _buttonText.text = "Selected";
                        ButtonSelect.interactable = false;
                    }
                    else
                    {
                        _buttonText.text = "Select";
                        ButtonSelect.interactable = true;
                    }

                    break;
                }
            case -12:
                {
                    if (!OpenSkin3)
                    {
                        _buttonText.text = "Buy for 200";

                        ButtonSelect.interactable = Menu.Coins >= 200;
                    }
                    else if (SelectSkin == 3)
                    {
                        _buttonText.text = "Selected";
                        ButtonSelect.interactable = false;
                    }
                    else
                    {
                        _buttonText.text = "Select";
                        ButtonSelect.interactable = true;
                    }

                    break;
                }
            case -18:
                {
                    if (!OpenSkin4)
                    {
                        _buttonText.text = "Buy for 400";
                        ButtonSelect.interactable = Menu.Coins >= 400;
                    }
                    else if (SelectSkin == 4)
                    {
                        _buttonText.text = "Selected";
                        ButtonSelect.interactable = false;
                    }
                    else
                    {
                        _buttonText.text = "Select";
                        ButtonSelect.interactable = true;
                    }

                    break;
                }
            case -24:
                {
                    if (!OpenSkin5)
                    {
                        _buttonText.text = "Buy for 800";
                        ButtonSelect.interactable = Menu.Coins >= 800;
                    }
                    else if (SelectSkin == 5)
                    {
                        _buttonText.text = "Selected";
                        ButtonSelect.interactable = false;
                    }
                    else
                    {
                        _buttonText.text = "Select";
                        ButtonSelect.interactable = true;
                    }

                    break;
                }
            case -30:
                {
                    if (!OpenSkin6)
                    {
                        _buttonText.text = "Buy for 1600";
                        ButtonSelect.interactable = Menu.Coins >= 1600;
                    }
                    else if (SelectSkin == 6)
                    {
                        _buttonText.text = "Selected";
                        ButtonSelect.interactable = false;
                    }
                    else
                    {
                        _buttonText.text = "Select";
                        ButtonSelect.interactable = true;
                    }

                    break;
                }
        }
    }

    private void FirstCaruselMove() 
    {
        switch (SelectSkin)
        {
            case 1:
                _endAnim = false;
                transform.DOMoveX(-Skins[SelectSkin-1].transform.localPosition.x, 1.5f).OnComplete(() => _endAnim = true);
                break;
            case 2:
                _endAnim = false;
                transform.DOMoveX(-Skins[SelectSkin-1].transform.localPosition.x, 1.5f).OnComplete(() => _endAnim = true);
                break;
            case 3:
                _endAnim = false;
                transform.DOMoveX(-Skins[SelectSkin-1].transform.localPosition.x, 1.5f).OnComplete(() => _endAnim = true);
                break;
            case 4:
                _endAnim = false;
                transform.DOMoveX(-Skins[SelectSkin-1].transform.localPosition.x, 1.5f).OnComplete(() => _endAnim = true);
                break;
            case 5:
                _endAnim = false;
                transform.DOMoveX(-Skins[SelectSkin-1].transform.localPosition.x, 1.5f).OnComplete(() => _endAnim = true);
                break;
            case 6:
                _endAnim = false;
                transform.DOMoveX(-Skins[SelectSkin-1].transform.localPosition.x, 1.5f).OnComplete(() => _endAnim = true);
                break;
        }
    }

    private void OnDisable()
    {
       MenuSwipes.SwipeEvent -= Input;
    }
}

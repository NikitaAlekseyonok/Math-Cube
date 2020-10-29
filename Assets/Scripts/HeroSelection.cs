// Copyright (c) 2012-2019 FuryLion Group. All Rights Reserved.

using UnityEngine;

public class HeroSelection : MonoBehaviour
{
    public GameObject Skin1;
    public GameObject Skin2;
    public GameObject Skin3;
    public GameObject Skin4;
    public GameObject Skin5;
    public GameObject Skin6;

    public void Start()
    {
        switch (SkinsCarusel.SelectSkin)
        {
            case 1:
                    Skin1.SetActive(true);
                    break;
            case 2:
                    Skin2.SetActive(true);
                    break;
            case 3:
                    Skin3.SetActive(true);
                    break;
            case 4:
                    Skin4.SetActive(true);
                    break;
            case 5:
                    Skin5.SetActive(true);
                    break;
            case 6:
                    Skin6.SetActive(true);
                    break;
        }
    }
}

// Copyright (c) 2012-2019 FuryLion Group. All Rights Reserved.

using UnityEngine;

public class PlaySound : MonoBehaviour
{
    public void Awake()
    {
        var objs = GameObject.FindGameObjectsWithTag("Music"); 
    
        if (objs.Length > 1) 
            Destroy(gameObject);
    
        DontDestroyOnLoad(gameObject);
    }
}

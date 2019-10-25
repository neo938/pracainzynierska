﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsOfPlayer: MonoBehaviour
{
    public static SettingsOfPlayer Instance;
    
    public static string nick="*Player";
    public static int lastUsedNetworkPort = 25000;
    public static bool firstTime=true;

    private void Awake()
    {
        if(Instance==null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else if(Instance!=this)
        {
            Destroy(gameObject);
        }
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GMoney : MonoBehaviour
{
    public static GMoney Instance;
    public int coins;

    private void Start()
    {
        Instance = this;
    }

    public void Awake()
    {
        LoadMoney();
               
    }

    public void SaveMoney()
    {
        PlayerPrefs.SetInt("Coins",coins);
        PlayerPrefs.Save();
        Debug.Log("Has Saved");
    }

    public void LoadMoney()
    {
        coins=PlayerPrefs.GetInt("Coins", 0);
    }

    public void ChangeCoins(int change)
    {
        coins += change;
        Debug.Log("Changes");
        SaveMoney();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    public EventSystem es;
    public Text txtCoin;
    int coinCount = 0;

    public void AddCoin()
    {
        coinCount++;
        txtCoin.text = coinCount.ToString();
    }
}

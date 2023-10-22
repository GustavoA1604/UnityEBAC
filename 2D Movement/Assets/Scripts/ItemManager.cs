using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public static ItemManager instance;
    public TextMeshProUGUI textCoins;
    public int coins = 0;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        Reset();
    }

    private void Reset()
    {
        SetCoins(0);
    }

    public void AddCoins(int number = 1)
    {
        SetCoins(coins + number);
    }

    private void SetCoins(int number)
    {
        coins = number;
        textCoins.SetText(coins.ToString());
    }
}

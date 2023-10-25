using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    private static ItemManager instance;
    public SOInt coins;

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
        coins.value = 0;
    }

    public static void AddCoins(int number = 1)
    {
        instance.coins.value += number;
    }
}

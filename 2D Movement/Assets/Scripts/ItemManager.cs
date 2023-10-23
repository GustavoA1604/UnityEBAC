using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public static ItemManager instance;
    public SOInt coins;
    public SOInt planets;

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
        planets.value = 0;
    }

    public void AddCoins(int number = 1)
    {
        coins.value += number;
    }

    public void AddPlanets(int number = 1)
    {
        planets.value += number;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinBase : CollectableBase
{
    protected override void OnCollect()
    {
        base.OnCollect();
        ItemManager.instance?.AddCoins();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpBase : CollectableBase
{
    [Header("Power Up")]
    public float duration = 1f;

    protected override void OnCollect()
    {
        base.OnCollect();
        GameObject.FindObjectOfType<PlayerController>().CollectPowerUp();
        StartPowerUp();
    }

    protected virtual void StartPowerUp()
    {
        Invoke(nameof(EndPowerUp), duration);
    }

    protected virtual void EndPowerUp()
    {
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpSpeedUp : PowerUpBase
{
    public float speedMultiplier = 1.5f;

    protected override void StartPowerUp()
    {
        base.StartPowerUp();
        GameObject.FindObjectOfType<PlayerController>().SetForwardSpeedMultiplier(speedMultiplier);
    }

    protected override void EndPowerUp()
    {
        base.EndPowerUp();
        GameObject.FindObjectOfType<PlayerController>().SetForwardSpeedMultiplier(1);
    }
}

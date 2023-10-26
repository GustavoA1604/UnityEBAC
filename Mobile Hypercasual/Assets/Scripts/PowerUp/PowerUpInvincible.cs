using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpInvincible : PowerUpBase
{

    protected override void StartPowerUp()
    {
        base.StartPowerUp();
        GameObject.FindObjectOfType<PlayerController>().SetInvincible(true);
    }

    protected override void EndPowerUp()
    {
        base.EndPowerUp();
        GameObject.FindObjectOfType<PlayerController>().SetInvincible(false);
    }
}

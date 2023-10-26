using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpJump : PowerUpBase
{
    public float jumpHeight = 2f;
    protected override void StartPowerUp()
    {
        base.StartPowerUp();
        GameObject.FindObjectOfType<PlayerController>().Jump(jumpHeight, duration);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class SOPlayerSetup : ScriptableObject
{

    [Header("Movement")]
    public float sideSpeed = 10f;
    public float sideRunningSpeed = 20f;
    public float sideFriction = .1f;
    public float jumpSpeed = 10f;

    [Header("Animation")]
    public Animator animator;
    public float jumpScaleY = 1.5f;
    public float jumpScaleX = .7f;
    public float jumpAnimationDuration = .3f;
    public string triggerRun = "Run";
    public string triggerDeath = "Death";
}


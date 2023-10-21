using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationSatellite : MonoBehaviour
{
    public Animator animator;
    public string anmTriggerFly = "Fly";
    public string anmTriggerLand = "Land";

    private void OnValidate()
    {
        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }
    }

    void Start()
    {
        Debug.Assert(animator != null);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            animator.SetTrigger(anmTriggerFly);
        }
        else if (Input.GetKeyDown(KeyCode.L))
        {
            animator.SetTrigger(anmTriggerLand);
        }
    }
}

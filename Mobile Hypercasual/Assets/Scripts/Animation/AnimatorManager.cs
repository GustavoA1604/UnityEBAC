using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorManager : MonoBehaviour
{
    public enum AnimationType
    {
        IDLE,
        RUN,
        DEAD
    }

    public Animator animator;
    public List<AnimatorSetup> listAnimatorSetup;

    private void GetAnimatorComponentIfNull()
    {
        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }
    }

    void OnValidate()
    {
        GetAnimatorComponentIfNull();
    }

    void Awake()
    {
        GetAnimatorComponentIfNull();
        Debug.Assert(animator != null);
    }

    public void Play(AnimationType type)
    {
        listAnimatorSetup.ForEach(item =>
        {
            if (item.type == type)
            {
                animator.SetTrigger(item.trigger);
            }
        });
    }
}

[System.Serializable]
public class AnimatorSetup
{
    public AnimatorManager.AnimationType type;
    public string trigger;
}
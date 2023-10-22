using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int damage = 10;
    public HealthBase myHealthBase;
    public Animator animator;
    public string triggerAttack = "Attack";
    public string triggerDeath = "Death";

    private void OnValidate()
    {
        if (myHealthBase == null)
        {
            myHealthBase = GetComponent<HealthBase>();
        }
    }

    private void Awake()
    {
        Debug.Assert(myHealthBase != null);
        myHealthBase.OnKill += OnEnemyKill;
    }

    private void OnEnemyKill()
    {
        myHealthBase.OnKill -= OnEnemyKill;
        PlayKillAnimation();
    }

    private void PlayKillAnimation()
    {
        animator?.SetTrigger(triggerDeath);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var playerHealthBase = collision.gameObject.GetComponent<HealthBase>();

        if (playerHealthBase != null)
        {
            playerHealthBase.Damage(damage);
            PlayAttackAnimation();
        }
    }

    private void PlayAttackAnimation()
    {
        animator?.SetTrigger(triggerAttack);
    }

    public void Damage(int damageVal)
    {
        myHealthBase.Damage(damageVal);
    }
}

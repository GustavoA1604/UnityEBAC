using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBase : MonoBehaviour
{
    public Action OnKill;
    public int startLife = 10;
    public bool destroyOnKill = false;
    public float timeToDestroy = 0f;
    private int _currentLife;
    private bool _isDead;
    private FlashColor _flashColor;

    void Awake()
    {
        _flashColor = GetComponent<FlashColor>();
        Init();
    }

    public void Init()
    {
        _currentLife = startLife;
        _isDead = false;
    }

    public void Damage(int damageVal)
    {
        if (_isDead) return;

        _currentLife -= damageVal;
        _flashColor?.Flash();
        if (_currentLife <= 0 && !_isDead)
        {
            Kill();
        }
    }

    private void Kill()
    {
        _isDead = true;
        if (destroyOnKill)
        {
            Destroy(gameObject, timeToDestroy);
        }
        OnKill?.Invoke();
    }
}

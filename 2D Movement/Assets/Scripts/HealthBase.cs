using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBase : MonoBehaviour
{
    public int startLife = 10;
    public bool destroyOnKill = false;
    private int _currentLife;
    private bool _isDead;

    void Awake()
    {
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
        if (_currentLife <= 0)
        {
            Kill();
        }
    }

    private void Kill()
    {
        _isDead = true;

        if (destroyOnKill)
        {
            Destroy(gameObject);
        }
    }
}

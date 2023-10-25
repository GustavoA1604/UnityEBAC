using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunBase : MonoBehaviour
{
    public ProjectileBase prefabProjectile;
    public Transform positionToShoot;
    public float timeBetweenShoots = .2f;
    //public Transform playerSideReference;
    private Transform _playerSideReference;
    private Coroutine _currentCoroutine = null;

    void Awake()
    {
        _playerSideReference = GameObject.FindObjectOfType<Player>().transform;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            Debug.Log("Im here");
            _currentCoroutine = StartCoroutine(StartShoot());
        }
        else if (Input.GetKeyUp(KeyCode.Z))
        {
            if (_currentCoroutine != null)
            {
                StopCoroutine(_currentCoroutine);
            }
        }
    }

    IEnumerator StartShoot()
    {
        while (true)
        {
            Shoot();
            yield return new WaitForSeconds(timeBetweenShoots);
        }
    }

    public void Shoot()
    {
        var projectile = Instantiate(prefabProjectile);
        projectile.transform.position = positionToShoot.position;
        projectile.invertDirection = _playerSideReference.transform.localScale.x < 0;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBase : MonoBehaviour
{
    public Vector3 direction = new Vector3(35, 0, 0);

    public float timeToDestroy = 1f;
    public bool invertDirection = false;
    public int damage = 1;

    private void Awake()
    {
        Destroy(gameObject, timeToDestroy);
    }

    void Update()
    {
        transform.Translate(direction * Time.deltaTime * (invertDirection ? -1 : 1));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Enemy enemy = collision.transform.GetComponent<Enemy>();

        if (enemy != null)
        {
            enemy.Damage(damage);
            Destroy(gameObject);
        }
    }
}

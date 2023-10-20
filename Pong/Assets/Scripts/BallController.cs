using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector2 startingVelocity = new Vector2(5f, 5f);
    public StatePlayingManager gameManager;
    public float speedUp = 1.05f;

    public void ResetBall()
    {
        transform.position = Vector3.zero;
        if (rb == null) rb = GetComponent<Rigidbody2D>();
        rb.velocity = startingVelocity;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            rb.velocity = new Vector2(rb.velocity.x, -rb.velocity.y);
        }
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Enemy"))
        {
            Vector2 toOther = transform.position - collision.transform.position;
            float angle = Vector2.Angle(Vector2.right, toOther);
            if (collision.gameObject.CompareTag("Player"))
            {
                angle = Math.Max(angle, 120f);
            }
            else
            {
                angle = Math.Min(angle, 60f);
            }
            if (transform.position.y < collision.transform.position.y)
            {
                angle *= -1;
            }
            float angleRad = angle * Mathf.Deg2Rad;
            float currSpeed = rb.velocity.magnitude;
            Vector2 newVelocity = new Vector2(Mathf.Cos(angleRad), Mathf.Sin(angleRad)) * currSpeed;
            rb.velocity = newVelocity * speedUp;
        }
        else if (collision.gameObject.CompareTag("WallEnemy"))
        {
            gameManager.ScorePlayer();
            ResetBall();
        }
        else if (collision.gameObject.CompareTag("WallPlayer"))
        {
            gameManager.ScoreEnemy();
            ResetBall();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Player : MonoBehaviour
{
    public Rigidbody2D myRigidBody;

    [Header("Movement")]
    public float sideSpeed = 10f;
    public float sideRunningSpeed = 20f;
    public float sideFriction = .1f;
    public float jumpSpeed = 10f;

    [Header("Animation")]
    public float jumpScaleY = 1.5f;
    public float jumpScaleX = .7f;
    public float jumpAnimationDuration = .3f;

    private bool _isJumpAnimationInProgress = false;

    void Start()
    {
        if (myRigidBody == null)
        {
            myRigidBody = GetComponent<Rigidbody2D>();
        }
        Debug.Assert(myRigidBody != null);
    }

    void Update()
    {
        HandleJump();
        HandleSideMovement();
    }

    private void HandleJump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            myRigidBody.velocity = new Vector2(myRigidBody.velocity.x, jumpSpeed);
            StartCoroutine(HandleScaleJumpAnimation());
        }
    }

    private IEnumerator HandleScaleJumpAnimation()
    {
        //myRigidBody.transform.DOScaleY(jumpScaleY, jumpAnimationDuration).SetLoops(2, LoopType.Yoyo);
        if (!_isJumpAnimationInProgress)
        {
            _isJumpAnimationInProgress = true;
            myRigidBody.transform.DOScaleX(jumpScaleX, jumpAnimationDuration / 2);
            myRigidBody.transform.DOScaleY(jumpScaleY, jumpAnimationDuration / 2);
            yield return new WaitForSeconds(jumpAnimationDuration / 2);
            myRigidBody.transform.DOScaleX(1, jumpAnimationDuration / 2);
            myRigidBody.transform.DOScaleY(1, jumpAnimationDuration / 2);
            yield return new WaitForSeconds(jumpAnimationDuration / 2);
            _isJumpAnimationInProgress = false;
        }
    }

    private void HandleSideMovement()
    {
        bool isSpeedKeyPressed = Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.LeftShift);
        float speed = isSpeedKeyPressed ? sideRunningSpeed : sideSpeed;

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            //rb.MovePosition(rb.position - sideVelocity * Time.deltaTime);
            myRigidBody.velocity = new Vector2(-speed, myRigidBody.velocity.y);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            //rb.MovePosition(rb.position + sideVelocity * Time.deltaTime);
            myRigidBody.velocity = new Vector2(speed, myRigidBody.velocity.y);
        }

        if (myRigidBody.velocity.x > sideFriction)
        {
            myRigidBody.velocity -= new Vector2(sideFriction, 0);
        }
        else if (myRigidBody.velocity.x < -sideFriction)
        {
            myRigidBody.velocity += new Vector2(sideFriction, 0);
        }
        else if (myRigidBody.velocity.x != 0)
        {
            myRigidBody.velocity = new Vector2(0, myRigidBody.velocity.y);
        }
    }
}

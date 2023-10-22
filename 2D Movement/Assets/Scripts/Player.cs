using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

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
    public Animator animator;
    public string triggerRun = "Run";
    public string triggerDeath = "Death";

    private bool _isJumpAnimationInProgress = false;
    public HealthBase myHealthBase;

    void Awake()
    {
        if (myRigidBody == null)
        {
            myRigidBody = GetComponent<Rigidbody2D>();
        }
        if (myHealthBase == null)
        {
            myHealthBase = GetComponent<HealthBase>();
        }
        Debug.Assert(myHealthBase != null);
        Debug.Assert(myRigidBody != null);
        Debug.Assert(animator != null);
    }

    void Start()
    {
        myHealthBase.OnKill += OnPlayerKill;
    }

    private void OnPlayerKill()
    {
        myHealthBase.OnKill -= OnPlayerKill;
        animator.SetTrigger(triggerDeath);
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
            float originalScaleX = myRigidBody.transform.localScale.x;
            _isJumpAnimationInProgress = true;
            myRigidBody.transform.DOScaleX(originalScaleX * jumpScaleX, jumpAnimationDuration / 2);
            myRigidBody.transform.DOScaleY(jumpScaleY, jumpAnimationDuration / 2);
            yield return new WaitForSeconds(jumpAnimationDuration / 2);
            myRigidBody.transform.DOScaleX(originalScaleX, jumpAnimationDuration / 2);
            myRigidBody.transform.DOScaleY(1, jumpAnimationDuration / 2);
            yield return new WaitForSeconds(jumpAnimationDuration / 2);
            _isJumpAnimationInProgress = false;
        }
    }

    private void HandleSideMovement()
    {
        bool isSpeedKeyPressed = Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.LeftShift);
        float speed = isSpeedKeyPressed ? sideRunningSpeed : sideSpeed;
        bool isMovingLeft = Input.GetKey(KeyCode.LeftArrow);
        bool isMovingRight = Input.GetKey(KeyCode.RightArrow);
        bool isMoving = isMovingLeft ^ isMovingRight;

        if (isMovingLeft && !isMovingRight)
        {
            //rb.MovePosition(rb.position - sideVelocity * Time.deltaTime);
            myRigidBody.velocity = new Vector2(-speed, myRigidBody.velocity.y);
            if (myRigidBody.transform.localScale.x > 0)
            {
                myRigidBody.transform.DOScaleX(-1, .1f);
            }
        }
        else if (isMovingRight && !isMovingLeft)
        {
            //rb.MovePosition(rb.position + sideVelocity * Time.deltaTime);
            myRigidBody.velocity = new Vector2(speed, myRigidBody.velocity.y);
            if (myRigidBody.transform.localScale.x < 0)
            {
                myRigidBody.transform.DOScaleX(1, .1f);
            }
        }
        animator.SetBool(triggerRun, isMoving);
        animator.speed = isMoving && isSpeedKeyPressed ? 2 : 1;

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

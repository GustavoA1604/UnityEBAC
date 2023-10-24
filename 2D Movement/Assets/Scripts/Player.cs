using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Player : MonoBehaviour
{
    public SOPlayerSetup soPlayerSetup;
    public Rigidbody2D myRigidBody;
    public HealthBase myHealthBase;
    private bool _isJumpAnimationInProgress = false;
    private Animator _currentPlayer;

    [Header("Jump Collision Check")]
    public float distToGround = .1f;
    [Header("VFX")]
    public ParticleSystem walkVFX;
    public ParticleSystem jumpVFX;

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
        Debug.Assert(soPlayerSetup != null);
        Debug.Assert(soPlayerSetup.animator != null);
        _currentPlayer = Instantiate(soPlayerSetup.animator, transform);
        _currentPlayer.transform.position = transform.position;
    }

    private bool IsOnGround()
    {
        return Physics2D.Raycast(transform.position, Vector2.down, distToGround);
    }

    void Start()
    {
        myHealthBase.OnKill += OnPlayerKill;
    }

    private void OnPlayerKill()
    {
        myHealthBase.OnKill -= OnPlayerKill;
        _currentPlayer.SetTrigger(soPlayerSetup.triggerDeath);
    }

    void Update()
    {
        if (walkVFX != null)
        {
            ParticleSystem.EmissionModule emission = walkVFX.emission;
            emission.enabled = IsOnGround();
        }
        HandleJump();
        HandleSideMovement();
    }

    private void HandleJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && IsOnGround())
        {
            myRigidBody.velocity = new Vector2(myRigidBody.velocity.x, soPlayerSetup.jumpSpeed);
            StartCoroutine(HandleScaleJumpAnimation());
        }
    }

    private IEnumerator HandleScaleJumpAnimation()
    {
        //myRigidBody.transform.DOScaleY(jumpScaleY, jumpAnimationDuration).SetLoops(2, LoopType.Yoyo);
        PlayJumpVFX();
        if (!_isJumpAnimationInProgress)
        {
            float originalScaleX = myRigidBody.transform.localScale.x;
            _isJumpAnimationInProgress = true;
            myRigidBody.transform.DOScaleX(originalScaleX * soPlayerSetup.jumpScaleX, soPlayerSetup.jumpAnimationDuration / 2);
            myRigidBody.transform.DOScaleY(soPlayerSetup.jumpScaleY, soPlayerSetup.jumpAnimationDuration / 2);
            yield return new WaitForSeconds(soPlayerSetup.jumpAnimationDuration / 2);
            myRigidBody.transform.DOScaleX(originalScaleX, soPlayerSetup.jumpAnimationDuration / 2);
            myRigidBody.transform.DOScaleY(1, soPlayerSetup.jumpAnimationDuration / 2);
            yield return new WaitForSeconds(soPlayerSetup.jumpAnimationDuration / 2);
            _isJumpAnimationInProgress = false;
        }
    }

    private void PlayJumpVFX()
    {
        if (jumpVFX != null)
        {
            jumpVFX.Play();
        }
    }

    private void HandleSideMovement()
    {
        bool isSpeedKeyPressed = Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.LeftShift);
        float speed = isSpeedKeyPressed ? soPlayerSetup.sideRunningSpeed : soPlayerSetup.sideSpeed;
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
        _currentPlayer.SetBool(soPlayerSetup.triggerRun, isMoving);
        _currentPlayer.speed = isMoving && isSpeedKeyPressed ? 2 : 1;

        if (myRigidBody.velocity.x > soPlayerSetup.sideFriction)
        {
            myRigidBody.velocity -= new Vector2(soPlayerSetup.sideFriction, 0);
        }
        else if (myRigidBody.velocity.x < -soPlayerSetup.sideFriction)
        {
            myRigidBody.velocity += new Vector2(soPlayerSetup.sideFriction, 0);
        }
        else if (myRigidBody.velocity.x != 0)
        {
            myRigidBody.velocity = new Vector2(0, myRigidBody.velocity.y);
        }
    }
}

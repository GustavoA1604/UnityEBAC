using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerController : MonoBehaviour
{
    [Header("Horizontal and Vertical Movement")]
    public Transform target;
    public float lerpSpeed = 1f;

    [Header("Forward Movement")]
    public float baseForwardSpeed = 5f;
    private float _forwardSpeedMultiplier = 1f;

    [Header("Gameplay")]
    public string tagEnemy = "Enemy";
    public string tagEndLine = "EndLine";
    public GameObject endScreen;

    private bool _canRun = false;

    private Vector3 _targetPos;
    private bool _isInvincible = false;

    [Header("Animation")]
    public AnimatorManager animatorManager;

    [Header("Particle")]
    public ParticleSystem particleSystemSpeedUp;
    public ParticleSystem particleSystemInvincible;

    private void GetAnimatorManagerComponent()
    {
        if (animatorManager == null)
        {
            animatorManager = GetComponent<AnimatorManager>();
        }
        if (animatorManager == null)
        {
            animatorManager = GetComponentInChildren<AnimatorManager>();
        }
    }
    void OnValidate()
    {
        GetAnimatorManagerComponent();
    }

    void Awake()
    {
        GetAnimatorManagerComponent();
    }

    private void SetEmission(ParticleSystem particleSystem, bool enabled)
    {
        if (particleSystem != null)
        {
            ParticleSystem.EmissionModule emission = particleSystem.emission;
            emission.enabled = enabled;
        }
    }

    void Start()
    {
        SetEmission(particleSystemSpeedUp, false);
        SetEmission(particleSystemInvincible, false);
    }

    void Update()
    {
        if (!_canRun)
        {
            return;
        }

        _targetPos = target.position;
        _targetPos.y = transform.position.y;
        _targetPos.z = transform.position.z;
        transform.position = Vector3.Lerp(transform.position, _targetPos, lerpSpeed * Time.deltaTime);
        transform.Translate(transform.forward * baseForwardSpeed * _forwardSpeedMultiplier * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag(tagEnemy) && !_isInvincible)
        {
            if (animatorManager != null)
            {
                animatorManager.Play(AnimatorManager.AnimationType.DEAD);
                transform.DOMoveZ(-1f, 1f).SetRelative();
            }
            EndGame();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag(tagEndLine))
        {
            if (animatorManager != null)
            {
                animatorManager.Play(AnimatorManager.AnimationType.IDLE);
            }
            EndGame();
        }
    }

    private void EndGame()
    {
        _canRun = false;
        endScreen.SetActive(true);
    }

    public void StartToRun()
    {
        _canRun = true;
        if (animatorManager != null)
        {
            animatorManager.Play(AnimatorManager.AnimationType.RUN);
        }
    }

    #region POWERUPS

    public void SetForwardSpeedMultiplier(float speedMultiplier)
    {
        _forwardSpeedMultiplier = speedMultiplier;
        SetEmission(particleSystemSpeedUp, speedMultiplier > 1);
    }

    public void SetInvincible(bool isInvincible)
    {
        _isInvincible = isInvincible;
        SetEmission(particleSystemInvincible, isInvincible);
    }

    public void Jump(float height, float duration)
    {
        StartCoroutine(JumpInternal(height, duration));
    }

    private IEnumerator JumpInternal(float height, float duration)
    {
        float transitionTime = .2f;
        transform.DOMoveY(transform.position.y + height, transitionTime).SetEase(Ease.InSine);
        yield return new WaitForSeconds(duration - transitionTime);
        transform.DOMoveY(transform.position.y - height, transitionTime).SetEase(Ease.InSine);
    }

    #endregion
}

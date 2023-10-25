using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Side Movement")]
    public Transform target;
    public float lerpSpeed = 1f;

    [Header("Forward Movement")]
    public float forwardSpeed = .1f;

    [Header("Gameplay")]
    public string tagEnemy = "Enemy";
    public string tagEndLine = "EndLine";
    public GameObject endScreen;

    private bool _canRun = false;

    private Vector3 _targetPos;

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
        transform.Translate(transform.forward * forwardSpeed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag(tagEnemy))
        {
            EndGame();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag(tagEndLine))
        {
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
    }
}

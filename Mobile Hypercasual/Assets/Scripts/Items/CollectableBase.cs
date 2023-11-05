using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableBase : MonoBehaviour
{
    public string compareTag = "Player";
    public GameObject graphicItem;

    [Header("Effects")]
    public ParticleSystem myParticleSystem;
    public AudioSource audioSourceOnCollect;
    public Vector3 rotationSpeed = new Vector3(0f, 0f, 0f);

    void Start()
    {
        if (rotationSpeed.y != 0)
        {
            graphicItem.gameObject.transform.Rotate(new Vector3(0, Random.Range(0, 360), 0));
        }
    }

    void Update()
    {
        graphicItem.gameObject.transform.Rotate(rotationSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.transform.CompareTag(compareTag))
        {
            Collect();
        }

    }

    protected virtual void Collect()
    {
        OnCollect();
        StartCoroutine(HideObject());
    }

    private IEnumerator HideObject()
    {
        graphicItem.gameObject.SetActive(false);
        GetComponent<Collider>().enabled = false;
        float sec = myParticleSystem == null ? 0 : myParticleSystem.main.duration;
        yield return new WaitForSeconds(sec);
        gameObject.SetActive(false);

    }

    protected virtual void OnCollect()
    {
        if (myParticleSystem != null)
        {
            myParticleSystem.Play();
        }
        if (audioSourceOnCollect != null)
        {
            audioSourceOnCollect.Play();
        }
    }

}

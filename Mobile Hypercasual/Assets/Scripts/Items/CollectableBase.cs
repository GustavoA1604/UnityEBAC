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

using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEditor.U2D;
using UnityEngine;
using DG.Tweening;

public class FlashColor : MonoBehaviour
{
    public List<SpriteRenderer> spriteRenderersList;
    public Color flashColor = Color.red;
    public float duration = .3f;

    private void OnValidate()
    {
        spriteRenderersList = new List<SpriteRenderer>();
        foreach (var child in GetComponentsInChildren<SpriteRenderer>())
        {
            spriteRenderersList.Add(child);
        }
    }

    public void Flash()
    {
        foreach (var s in spriteRenderersList)
        {
            s.DOColor(flashColor, duration).SetLoops(2, LoopType.Yoyo);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class LevelPiece : MonoBehaviour
{
    public Transform end;

    void OnValidate()
    {
        end = transform.Find("End");
    }

    void Awake()
    {
        Assert.IsNotNull(end, "End of object piece not defined");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtManager : MonoBehaviour
{
    public enum ArtThemeT
    {
        AZULE,
        MUSHROOM
    }

    private static ArtManager instance;
    public ArtThemeT theme = ArtThemeT.AZULE;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        int random = Random.Range(0, 2);
        theme = random == 0 ? ArtThemeT.AZULE : ArtThemeT.MUSHROOM;
    }

    public static ArtThemeT GetTheme()
    {
        return instance.theme;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class ArtPiece : MonoBehaviour
{
    public List<ArtPieceSetup> artPieces;

    void Awake()
    {
        MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
        if (meshRenderer != null)
        {
            meshRenderer.enabled = false;
        }
    }

    void Start()
    {
        ArtManager.ArtThemeT currentTheme = ArtManager.GetTheme();
        ArtPieceSetup setup = artPieces.Find(p => p.theme == currentTheme);
        if (setup == null)
        {
            setup = artPieces[0];
        }
        Assert.IsNotNull(setup, "Cannot create art, null setup");

        GameObject selectedArt = setup.prefabs[Random.Range(0, setup.prefabs.Count)];
        GameObject instantiatedArt = GameObject.Instantiate(selectedArt, transform);
        instantiatedArt.transform.position = transform.position;
    }
}

[System.Serializable]
public class ArtPieceSetup
{
    public ArtManager.ArtThemeT theme;
    public List<GameObject> prefabs;
}
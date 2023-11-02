using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class LevelManager : MonoBehaviour
{
    //public Transform container;
    //public List<GameObject> levels;
    //[SerializeField] private int _index = 0;
    private GameObject _currentLevel = null;

    /* private void Awake()
    {
        SpawnNextLevel();
    } */

    /* private void SpawnNextLevel()
    {
        if (_currentLevel != null)
        {
            Destroy(_currentLevel);
            _index = (_index + 1) % levels.Count;
        }
        _currentLevel = Instantiate(levels[_index], container);
        _currentLevel.transform.localPosition = Vector3.zero;
    } */

    public void InstantiateLevel(SOLevel level)
    {
        Assert.IsNotNull(level, "Could not instantiate level, null pointer reference");
        if (level == null)
        {
            return;
        }

        DestroyCurrentLevel();

        _currentLevel = GameObject.Instantiate(new GameObject(), Vector3.zero, Quaternion.identity);
        _currentLevel.transform.parent = transform;

        GameObject startPiece = Instantiate(level.startPiece.gameObject, _currentLevel.transform);
        startPiece.transform.position = _currentLevel.transform.position;
        Transform pieceEnd = startPiece.GetComponent<LevelPiece>().end;

        pieceEnd = InstantiateLevelContents(level, pieceEnd);

        GameObject endPiece = Instantiate(level.endPiece.gameObject, _currentLevel.transform);
        endPiece.transform.position = pieceEnd.transform.position;
    }

    private Transform InstantiateLevelContents(SOLevel level, Transform pieceEnd)
    {
        switch (level.levelGenType)
        {
            case LevelGenerationTypeT.RANDOM_ORDER:
            case LevelGenerationTypeT.FIXED:
                if (level.levelGenType == LevelGenerationTypeT.RANDOM_ORDER)
                {
                    int n = level.levelPieces.Count;
                    while (n > 1)
                    {
                        n--;
                        int k = Random.Range(0, n + 1);
                        (level.levelPieces[n], level.levelPieces[k]) = (level.levelPieces[k], level.levelPieces[n]);
                    }

                }
                foreach (LevelPiece piece in level.levelPieces)
                {
                    GameObject instantiatedPiece = Instantiate(piece.gameObject, _currentLevel.transform);
                    instantiatedPiece.transform.position = pieceEnd.transform.position;
                    pieceEnd = instantiatedPiece.GetComponent<LevelPiece>().end;
                }
                break;
            case LevelGenerationTypeT.RANDOM_CONTENTS:
                for (int i = 0; i < level.numberOfPieces; i++)
                {
                    int k = Random.Range(0, level.levelPieces.Count - 1);
                    GameObject instantiatedPiece = Instantiate(level.levelPieces[k].gameObject, _currentLevel.transform);
                    instantiatedPiece.transform.position = pieceEnd.transform.position;
                    pieceEnd = instantiatedPiece.GetComponent<LevelPiece>().end;
                }
                break;
        }
        return pieceEnd;
    }

    public void DestroyCurrentLevel()
    {
        if (_currentLevel != null)
        {
            Destroy(_currentLevel);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Assertions;

public class LevelManager : MonoBehaviour
{
    [Header("Animation")]
    public float timeToCreatePiece = .2f;
    public float timeBetweenPieceCreation = .15f;
    public Ease pieceCreationEase = Ease.OutBack;

    private GameObject _currentLevel = null;
    private GameObject _startPiece = null;
    private List<GameObject> _middlePieces = new List<GameObject>();
    private GameObject _endPiece = null;


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

        _startPiece = Instantiate(level.startPiece.gameObject, _currentLevel.transform);
        _startPiece.transform.position = _currentLevel.transform.position;
        _startPiece.SetActive(false);
        Transform localPieceEnd = _startPiece.GetComponent<LevelPiece>().end;

        localPieceEnd = InstantiateLevelContents(level, localPieceEnd);

        _endPiece = Instantiate(level.endPiece.gameObject, _currentLevel.transform);
        _endPiece.transform.position = localPieceEnd.transform.position;

        StartCoroutine(AnimateLevelInstantiation());
    }

    private Transform InstantiateLevelContents(SOLevel level, Transform localPieceEnd)
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
                    localPieceEnd = InstantiateMiddlePiece(piece, localPieceEnd);
                }
                break;
            case LevelGenerationTypeT.RANDOM_CONTENTS:
                for (int i = 0; i < level.numberOfPieces; i++)
                {
                    int k = Random.Range(0, level.levelPieces.Count - 1);
                    localPieceEnd = InstantiateMiddlePiece(level.levelPieces[k], localPieceEnd);
                }
                break;
        }
        return localPieceEnd;
    }

    private Transform InstantiateMiddlePiece(LevelPiece piece, Transform localPieceEnd)
    {
        GameObject instantiatedPiece = Instantiate(piece.gameObject, _currentLevel.transform);
        instantiatedPiece.transform.position = localPieceEnd.transform.position;
        _middlePieces.Add(instantiatedPiece);
        return instantiatedPiece.GetComponent<LevelPiece>().end;
    }

    private IEnumerator AnimateLevelInstantiation()
    {
        yield return null;

        foreach (GameObject piece in _middlePieces)
        {
            piece.transform.localScale = Vector3.zero;
        }
        _endPiece.transform.localScale = Vector3.zero;

        for (int i = 0; i < _middlePieces.Count; i++)
        {
            _middlePieces[i].transform.DOScale(1, timeToCreatePiece).SetEase(pieceCreationEase);
            yield return new WaitForSeconds(timeBetweenPieceCreation);
        }
        _endPiece.transform.DOScale(1, timeToCreatePiece).SetEase(pieceCreationEase);
    }

    public void DestroyCurrentLevel()
    {
        if (_currentLevel != null)
        {
            Destroy(_currentLevel);
            _startPiece = null;
            _middlePieces.Clear();
            _endPiece = null;
        }
    }
}

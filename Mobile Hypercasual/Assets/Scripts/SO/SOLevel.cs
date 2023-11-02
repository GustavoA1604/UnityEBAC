using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class SOLevel : ScriptableObject
{
    public LevelPiece startPiece;
    public LevelPiece endPiece;
    public List<LevelPiece> levelPieces;
    public LevelGenerationTypeT levelGenType;
    public int numberOfPieces = 1;
}

public enum LevelGenerationTypeT
{
    FIXED,
    RANDOM_ORDER,
    RANDOM_CONTENTS
}
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StatePlayingManager : MonoBehaviour
{
  public GameManager gameManager;
  public Transform playerPaddle;
  public Transform enemyPaddle;
  public BallController ballController;

  public int playerScore = 0;
  public int enemyScore = 0;
  public TextMeshProUGUI textPointsPlayer;
  public TextMeshProUGUI textPointsEnemy;

  public int winPoints = 3;
  public void CheckWin()
  {
    if (enemyScore >= winPoints || playerScore >= winPoints)
    {
      gameManager.ReportGameFinished(playerScore, enemyScore, winPoints);
    }
  }

  void Start() { ResetGame(); }
  void OnEnable() { ResetGame(); }

  public void ResetGame()
  {
    // Define as posições iniciais das raquetes  
    playerPaddle.position = new Vector3(7f, 0f, 0f);
    enemyPaddle.position = new Vector3(-7f, 0f, 0f);
    ballController.ResetBall();
    playerScore = 0;
    enemyScore = 0;
    textPointsEnemy.text = enemyScore.ToString();
    textPointsPlayer.text = playerScore.ToString();
  }

  public void ScorePlayer()
  {
    playerScore++;
    textPointsPlayer.text = playerScore.ToString();
    CheckWin();
  }
  public void ScoreEnemy()
  {
    enemyScore++;
    textPointsEnemy.text = enemyScore.ToString();
    CheckWin();
  }
}

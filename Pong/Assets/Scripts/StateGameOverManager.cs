using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StateGameOverManager : MonoBehaviour
{
  public TextMeshProUGUI textFinalScore;

  public void Setup(int playerScore, int enemyScore, int winPoints)
  {
    bool playerWon = winPoints == playerScore;
    string msg = playerWon ? "Player" : "Computer";
    msg += " won\n";
    msg += "Player: " + playerScore + " points\n";
    msg += "Computer: " + enemyScore + " points";
    textFinalScore.text = msg;
  }
}

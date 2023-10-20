using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameStateT
{
    Menu,
    Playing,
    GameOver,
    About
}

public class GameManager : MonoBehaviour
{
    public GameStateT initialState = GameStateT.Menu;
    public GameObject menuGameObject;
    public GameObject playingGameObject;
    public GameObject gameOverGameObject;
    public GameObject aboutGameObject;

    private GameStateT _currentState;

    void Awake()
    {
        menuGameObject.SetActive(false);
        playingGameObject.SetActive(false);
        gameOverGameObject.SetActive(false);
        aboutGameObject.SetActive(false);
        EnterGameState(initialState);
    }

    private void ExitGameState()
    {
        switch (_currentState)
        {
            case GameStateT.Menu:
                menuGameObject.SetActive(false);
                break;
            case GameStateT.Playing:
                playingGameObject.SetActive(false);
                break;
            case GameStateT.GameOver:
                gameOverGameObject.SetActive(false);
                break;
            case GameStateT.About:
                aboutGameObject.SetActive(false);
                break;
        }
    }

    private void EnterGameState(GameStateT newState)
    {
        _currentState = newState;
        switch (newState)
        {
            case GameStateT.Menu:
                menuGameObject.SetActive(true);
                break;
            case GameStateT.Playing:
                playingGameObject.SetActive(true);
                break;
            case GameStateT.GameOver:
                gameOverGameObject.SetActive(true);
                break;
            case GameStateT.About:
                aboutGameObject.SetActive(true);
                break;
        }
    }

    private void MakeGameStateTransition(GameStateT newState)
    {
        ExitGameState();
        EnterGameState(newState);
    }

    public void MakeGameStateTransitionToMenu()
    {
        MakeGameStateTransition(GameStateT.Menu);
    }
    public void MakeGameStateTransitionToPlaying()
    {
        MakeGameStateTransition(GameStateT.Playing);
    }
    public void MakeGameStateTransitionToGameOver()
    {
        MakeGameStateTransition(GameStateT.GameOver);
    }
    public void MakeGameStateTransitionToAbout()
    {
        MakeGameStateTransition(GameStateT.About);
    }

    public void ReportGameFinished(int playerScore, int enemyScore, int winPoints)
    {
        StateGameOverManager gameOverManager = gameOverGameObject.GetComponent<StateGameOverManager>();
        gameOverManager.Setup(playerScore, enemyScore, winPoints);
        MakeGameStateTransitionToGameOver();
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}

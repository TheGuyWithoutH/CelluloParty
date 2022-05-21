using System;
using System.Collections;
using System.Collections.Generic;
using Game.Cellulos;
using Game.Mini_Games;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    public CelluloPlayer player1;
    private int _player1Tile;
    public CelluloPlayer player2;
    private int _player2Tile;

    public Mini_Game curling;
    public Mini_Game mole;
    public Mini_Game quiz;
    public Mini_Game simonSays;
    private bool _miniGameRunning;

    public Camera mainCamera;
    public Camera player1Camera;
    public Camera player2Camera;

    private GameState _state;

    public void Awake()
    {
        EnableCamera(CameraView.MainCamera);
    }

    // Start is called before the first frame update
    void Start()
    {
        _player1Tile = 0;
        _player2Tile = 0;
        _state = GameState.Start;
        _miniGameRunning = false;
    }

    // Update is called once per frame
    void Update()
    {
        switch (_state)
        {
            case GameState.Start:
                if (player1.IsReady && player2.IsReady)
                {
                    
                    _state = GameState.DiceRoll;
                }
                break;
            case GameState.MiniGame:
                if (!_miniGameRunning)
                {
                    _miniGameRunning = true;
                    MiniGame randomGame = (MiniGame)Random.Range(0.0f, 3.0f);

                    switch (randomGame)
                    {
                        case MiniGame.Curling :
                            curling.StartGame();
                            break;
                        case MiniGame.Mole:
                            mole.StartGame();
                            break;
                        case MiniGame.Quiz:
                            quiz.StartGame();
                            break;
                        case MiniGame.SimonSays:
                            simonSays.StartGame();
                            break;
                    }
                }
                break;
            case GameState.DiceRoll:
                break;
            case GameState.Podium:
                break;
            case GameState.End:
                break;
        }
    } 

    private void EnableCamera(CameraView cameraChoice)
    {
        switch (cameraChoice)
        {
            case CameraView.MainCamera :
                mainCamera.enabled = true;
                player1Camera.enabled = false;
                player2Camera.enabled = false;
                break;
            case CameraView.Player1:
                mainCamera.enabled = false;
                player1Camera.enabled = true;
                player2Camera.enabled = false;
                break;
            case CameraView.Player2:
                mainCamera.enabled = false;
                player1Camera.enabled = false;
                player2Camera.enabled = true;
                break;
            default:
                mainCamera.enabled = true;
                player1Camera.enabled = false;
                player2Camera.enabled = false;
                break;
        }
    }

    public void MiniGameQuit(Winner winner)
    {
        
    }

    private enum CameraView
    {
        MainCamera,
        Player1,
        Player2,
    }

    private enum GameState
    {
        Start,
        DiceRoll,
        MiniGame,
        Podium,
        End
    }

    private enum MiniGame
    {
        Curling,
        Mole,
        Quiz,
        SimonSays
    }
}

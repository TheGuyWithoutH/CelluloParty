using System;
using System.Collections;
using System.Collections.Generic;
using Game.Cellulos;
using Game.Dices;
using Game.Map;
using Game.Mini_Games;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    public CelluloPlayer player1;
    private int _player1Tile;
    public CelluloPlayer player2;
    private int _player2Tile;
    private Winner _currentWinner;

    public Mini_Game curling;
    public Mini_Game mole;
    public Mini_Game quiz;
    public Mini_Game simonSays;
    private bool _miniGameRunning;

    public Dice normalDice;
    public Dice winnerDice;
    public Dice looserDice;
    private Dice _currentDice;
    private int _diceResultPlayer1;
    private int _diceResultPlayer2;

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
        _currentWinner = Winner.NONE;
        _currentDice = normalDice;
        
        player1.SetNotReady();
        player2.SetNotReady();
    }

    // Update is called once per frame
    void Update()
    {
        switch (_state)
        {
            case GameState.Start:
                if (player1.IsReady && player2.IsReady)
                {
                    _state = GameState.DiceRollPlayer1;
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
            case GameState.DiceRollPlayer1:
                
                if (_currentWinner == Winner.PLAYER1)
                {
                    SetDiceThrow(winnerDice);
                }
                else if (_currentWinner == Winner.PLAYER2)
                {
                    SetDiceThrow(looserDice);
                }
                else
                {
                    SetDiceThrow(normalDice);
                }
                    
                _currentDice.ThrowDice();
                break;
            case GameState.DiceRollPlayer2:
                    if (_currentDice.DiceThrowDone())
                    {
                        _diceResultPlayer1 = _currentDice.GetDiceScore();

                        if (_currentWinner == Winner.PLAYER2)
                        {
                            SetDiceThrow(winnerDice);
                        }
                        else if (_currentWinner == Winner.PLAYER1)
                        {
                            SetDiceThrow(looserDice);
                        }
                        else
                        {
                            SetDiceThrow(normalDice);
                        }
                    
                        _currentDice.ThrowDice();
                    }
                
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

    private void SetDiceThrow(Dice dice)
    {
        if (dice == normalDice)
        {
            normalDice.enabled = true;
            looserDice.enabled = false;
            winnerDice.enabled = false;
            _currentDice = normalDice;
        } else if (dice == winnerDice) 
        {
            normalDice.enabled = false;
            looserDice.enabled = false;
            winnerDice.enabled = true;
            _currentDice = winnerDice;
        }
        else
        {
            normalDice.enabled = false;
            looserDice.enabled = true;
            winnerDice.enabled = false;
            _currentDice = looserDice;
        }
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
        MiniGame,
        DiceRollPlayer1,
        MovementPlayer1,
        DiceRollPlayer2,
        MovementPlayer2,
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

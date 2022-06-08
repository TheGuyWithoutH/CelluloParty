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
    private GameCell _player1Tile;
    public CelluloPlayer player2;
    private GameCell _player2Tile;
    private Player _currentWinner;
    private int _round;

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
    private bool _diceThrown;

    public Camera mainCamera;
    public Camera player1Camera;
    public Camera player2Camera;

    private GameState _state;

    public void Awake()
    {
        EnableCamera(CameraView.MainCamera);
        SetDiceThrow(null);

        //default case not managed
        if (PlayerPrefs.HasKey("name1"))
        {
            player1.playerName = getName("name1");
        }
        else
        {
            player1.playerName = "Player 1";
        }

        if (PlayerPrefs.HasKey("name2"))
        {
            player2.playerName = getName("name2");
        }
        else
        {
            player2.playerName = "Player 2";
        }

        if (PlayerPrefs.HasKey("couleur1"))
        {
            player1.celluloAgent.SetVisualEffect(VisualEffect.VisualEffectConstAll, getColor("couleur1"), 255);
        }
        else
        {
            player1.celluloAgent.SetVisualEffect(VisualEffect.VisualEffectConstAll, Color.red, 255);
        }

        if (PlayerPrefs.HasKey("couleur2"))
        {
            player2.celluloAgent.SetVisualEffect(VisualEffect.VisualEffectConstAll, getColor("couleur2"), 255);
        }
        else
        {
            player2.celluloAgent.SetVisualEffect(VisualEffect.VisualEffectConstAll, Color.blue, 255);
        }



    }

    // Start is called before the first frame update
    void Start()
    {
        _state = GameState.None;
        _player1Tile = GameCell.Cell1;
        _player2Tile = GameCell.Cell1;
        ExecuteAfterDelay(10, () =>
        {
            Debug.Log("10s after");
            _state = GameState.Start;
        });
        _miniGameRunning = false;
        _currentWinner = Player.NONE;
        _diceThrown = false;
        _round = 1;

        player1.SetNotReady();
        player2.SetNotReady();
        DisplayStart(true);
    }

    // Update is called once per frame
    void Update()
    {
        switch (_state)
        {
            case GameState.Start:
                /*GameCell.Cell10.SetCellOccupied(true);
                Debug.Log(GameCell.Cell10.GetCellOccupied());
                player1.player.SetGoalPosition(GameCell.Cell1.GetCellPosition().x, GameCell.Cell1.GetCellPosition().z, 1);
                ExecuteAfterDelay(5, () => player1.SetTargetCell(GameCell.Cell10));
                _state = GameState.End;*/
                Debug.Log("start the game");
                _miniGameRunning = true;
                quiz.StartGame();
                _state = GameState.MiniGame;
                
                //////////////////////////////////////////////////////////////////
                
                /*if (player1.IsReady && player2.IsReady)
                {
                    DisplayStart(false);
                    player1.SetNotReady();
                    player2.SetNotReady();
                    _state = GameState.DiceRollPlayer1;
                }*/
                break;
            case GameState.MiniGame:
                if (!_miniGameRunning)
                {
                    _miniGameRunning = true;
                    DisplayMiniGame(true);
                    
                    ExecuteAfterDelay(5, () =>
                    {
                        DisplayMiniGame(false);
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
                    });
                }
                break;
            case GameState.DiceRollPlayer1:
                if (player1.IsReady)
                {
                    if (_currentWinner == Player.PLAYER1)
                    {
                        SetDiceThrow(winnerDice);
                    }
                    else if (_currentWinner == Player.PLAYER2)
                    {
                        SetDiceThrow(looserDice);
                    }
                    else
                    {
                        SetDiceThrow(normalDice);
                    }
                                        
                    _currentDice.ThrowDice();
                    _diceThrown = true;
                    player1.SetNotReady();
                }
                else if (_diceThrown)
                {
                    if (_currentDice.DiceThrowDone())
                    {
                        _diceResultPlayer1 = _currentDice.GetDiceScore();
                        EnableCamera(CameraView.Player1);
                        _player1Tile += _diceResultPlayer1;
                        player1.SetTargetCell(_player1Tile);
                        _state = GameState.MovementPlayer1;
                        _diceThrown = false;
                        SetDiceThrow(null);
                    }
                }
                break;
            case GameState.DiceRollPlayer2:
                if (player2.IsReady)
                {
                    if (_currentWinner == Player.PLAYER2)
                    {
                        SetDiceThrow(winnerDice);
                    }
                    else if (_currentWinner == Player.PLAYER1)
                    {
                        SetDiceThrow(looserDice);
                    }
                    else
                    {
                        SetDiceThrow(normalDice);
                    }
                                        
                    _currentDice.ThrowDice();
                    _diceThrown = true;
                }
                else if (_diceThrown)
                {
                    if (_currentDice.DiceThrowDone())
                    {
                        player2.SetNotReady();
                        _diceResultPlayer2 = _currentDice.GetDiceScore();
                        EnableCamera(CameraView.Player2);
                        _player2Tile += _diceResultPlayer2;
                        player2.SetTargetCell(_player2Tile);
                        _state = GameState.MovementPlayer2;
                        _diceThrown = false;
                        SetDiceThrow(null);
                    }
                }
                break;
            case GameState.MovementPlayer1:
                if (player1.MoveIsDone())
                {
                    if (_player1Tile > GameCell.Cell40)
                    {
                        ExecuteAfterDelay(3, () =>
                        {
                            EnableCamera(CameraView.MainCamera);
                            _state = GameState.End;
                        });
                    }
                    else
                    {
                        ExecuteAfterDelay(3, () =>
                        {
                            EnableCamera(CameraView.MainCamera);
                            _state = GameState.DiceRollPlayer2;
                        });
                    }
                    
                }
                break;
            case GameState.MovementPlayer2:
                if (player2.MoveIsDone())
                {
                    if (_player2Tile > GameCell.Cell40)
                    {
                        ExecuteAfterDelay(3, () =>
                        {
                            EnableCamera(CameraView.MainCamera);
                            _state = GameState.End;
                        });
                    }
                    else
                    {
                        ExecuteAfterDelay(3, () =>
                        {
                            EnableCamera(CameraView.MainCamera);
                            _state = GameState.AdditionalFeatures;
                        });
                    }
                }
                break;
            case GameState.AdditionalFeatures:
                DisplayEndRound(true);
                ExecuteAfterDelay(5, () =>
                {
                    DisplayEndRound(false);
                    ++_round;
                    _state = GameState.MiniGame;
                });
                _state = GameState.None;
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

    public void MiniGameQuit(Player winner)
    {
        _currentWinner = winner;
        player1.GoBackInCell();
        player2.GoBackInCell();
        ExecuteAfterDelay(10, () =>
        {
            _miniGameRunning = false;
            _state = GameState.DiceRollPlayer1;
        });
    }

    private void SetDiceThrow(Dice dice)
    {
        if (dice == normalDice)
        {
            normalDice.gameObject.SetActive(true);
            looserDice.gameObject.SetActive(false);
            winnerDice.gameObject.SetActive(false);
            _currentDice = normalDice;
        } else if (dice == winnerDice) 
        {
            normalDice.gameObject.SetActive(false);
            looserDice.gameObject.SetActive(false);
            winnerDice.gameObject.SetActive(true);
            _currentDice = winnerDice;
        }
        else if(dice == looserDice)
        {
            normalDice.gameObject.SetActive(false);
            looserDice.gameObject.SetActive(true);
            winnerDice.gameObject.SetActive(false);
            _currentDice = looserDice;
        }
        else
        {
            normalDice.gameObject.SetActive(false);
            looserDice.gameObject.SetActive(false);
            winnerDice.gameObject.SetActive(false);
            _currentDice = null;
        }
    }
    
    private void DisplayStart(bool disp)
    {
        
    }

    private void DisplayMiniGame(bool disp)
    {
        
    }

    private void DisplayEndRound(bool disp)
    {
        
    }

    private string getName(string name)
    {
        return PlayerPrefs.GetString(name);
    }

    private Color getColor(string name)
    {
        switch (PlayerPrefs.GetInt(name))
        {
            case 0: return Color.red;
            case 1: return Color.blue;
            case 2: return Color.green;
            default: return Color.white;
        }
            
    }

    //============================= Useful Enums Across Game =======================================//
    
    private enum CameraView
    {
        MainCamera,
        Player1,
        Player2,
    }

    private enum GameState
    {
        None = -1,
        Start,
        MiniGame,
        DiceRollPlayer1,
        MovementPlayer1,
        DiceRollPlayer2,
        MovementPlayer2,
        AdditionalFeatures,
        End
    }

    private enum MiniGame
    {
        Curling,
        Mole,
        Quiz,
        SimonSays
    }
    
    public enum Player
    {
        NONE,
        PLAYER1,
        PLAYER2
    }
    
    //****************************************************************************************//
    //************************ Helper Method For Delayed Execution ***************************//
    //****************************************************************************************//
    
    private bool _isCoroutineExecuting = false;
        
    private IEnumerator ExecuteAfterTime(float time, Action task)
    {
        if (_isCoroutineExecuting)
            yield break;
        _isCoroutineExecuting = true;
        yield return new WaitForSeconds(time);
        task();
        _isCoroutineExecuting = false;
    }

    private void ExecuteAfterDelay(float time, Action task)
    {
        StartCoroutine(ExecuteAfterTime(time, task));
    }
}
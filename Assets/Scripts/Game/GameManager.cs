using System;
using System.Collections;
using Game.Cellulos;
using Game.Dices;
using Game.Map;
using Game.Mini_Games;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    public CelluloPlayer player1;
    private GameCell _player1Tile;
    public CelluloPlayer player2;
    private GameCell _player2Tile;
    private Player _currentWinner;
    private int _round;
    private bool _specialMove;
    private bool _isInSpecialMove;

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

    public Image startScreen;
    public Image startMiniGameScreen;
    public Image endMiniGameScreen;
    public TextMeshProUGUI currentWinner;
    public Image endRoundScreen;
    public Image endScreen;
    public TextMeshProUGUI winner;
    public TextMeshProUGUI looser;
    public TextMeshProUGUI infos;

    public AudioSource background;
    public AudioSource backgroundGames;
    public AudioSource cling;
    public AudioSource gameEffect;
    

    private GameState _state;

    private const string DiceText = "To throw the dice, press on the Cellulo";
    
    public void Awake()
    {
        EnableCamera(CameraView.MainCamera);
        SetDiceThrow(null);
        
        player1.playerName = PlayerPrefs.HasKey("name1") ? getName("name1") : "Player 1";
        player1.GetComponentInChildren<TextMesh>().text = player1.playerName;

        
        player2.playerName = PlayerPrefs.HasKey("name2") ? getName("name2") : "Player 2";
        player2.GetComponentInChildren<TextMesh>().text = player2.playerName;

        player1.celluloAgent.initialColor = PlayerPrefs.HasKey("couleur1") ? getColor("couleur1") : Color.red;

        player2.celluloAgent.initialColor = PlayerPrefs.HasKey("couleur2") ? getColor("couleur2") : Color.blue;
    }

    // Start is called before the first frame update
    void Start()
    {
        _state = GameState.None;
        _player1Tile = GameCell.Cell1;
        _player2Tile = GameCell.Cell1;
        _miniGameRunning = false;
        _currentWinner = Player.NONE;
        _diceThrown = false;
        _round = 1;
        _specialMove = false;
        _isInSpecialMove = false;

        player1.SetNotReady();
        player2.SetNotReady();
        DisplayStart(true);

        ExecuteAfterDelay(5, () =>
        {
            Debug.Log("10s after");
            _state = GameState.MiniGame;
        });
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
                
                /*Debug.Log("start the game");
                _miniGameRunning = true;
                quiz.StartGame();
                _state = GameState.MiniGame;*/
                
                //////////////////////////////////////////////////////////////////
                
                if (player1.IsReady && player2.IsReady)
                {
                    DisplayStart(false);
                    Debug.Log("goooo");
                    player1.SetNotReady();
                    player2.SetNotReady();
                    player1.GoBackInCell();
                    player2.GoBackInCell();
                    ExecuteAfterDelay(10, () =>
                    {
                        infos.text = DiceText;
                        infos.gameObject.SetActive(true);
                        _state = GameState.DiceRollPlayer1;
                    });
                }
                break;
            case GameState.MiniGame:
                if (!_miniGameRunning)
                {
                    _miniGameRunning = true;
                    DisplayMiniGame(true);
                    gameEffect.Play();
                    
                    ExecuteAfterDelay(5, () =>
                    {
                        DisplayMiniGame(false);
                        background.Stop();
                        backgroundGames.Play();
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
                    Debug.Log("Player 1 throw");
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
                    infos.gameObject.SetActive(false);
                    _diceThrown = true;
                    player1.SetNotReady();
                }
                else if (_diceThrown)
                {
                    if (_currentDice.DiceThrowDone())
                    {
                        cling.Play();
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
                    infos.gameObject.SetActive(false);
                    _diceThrown = true;
                }
                else if (_diceThrown)
                {
                    if (_currentDice.DiceThrowDone())
                    {
                        cling.Play();
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
                            _currentWinner = Player.PLAYER1;
                            _state = GameState.End;
                        });
                    }
                    else
                    {
                        ExecuteAfterDelay(3, () =>
                        {
                            EnableCamera(CameraView.MainCamera);
                            _currentWinner = Player.PLAYER2;
                            infos.text = DiceText;
                            infos.gameObject.SetActive(true);
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
                if (!_isInSpecialMove)
                {
                    CheckForSpecialCells();
                    if (_specialMove)
                    {
                        OrderSpecialMove();
                        _isInSpecialMove = true;
                    }
                    else
                    {
                        DisplayEndRound(true);
                        ExecuteAfterDelay(5, () =>
                        {
                            DisplayEndRound(false);
                            ++_round; 
                            _state = GameState.MiniGame;
                        });
                        _state = GameState.None;
                    }
                                    
                }
                break;
            case GameState.End:
                winner.text = _currentWinner == Player.PLAYER1 ? player1.playerName : player2.playerName;
                looser.text = _currentWinner == Player.PLAYER1 ? player2.playerName : player1.playerName;
                endScreen.gameObject.SetActive(true);
                _state = GameState.None;
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
        currentWinner.text = _currentWinner == Player.PLAYER1 ? player1.playerName : player2.playerName;
        endMiniGameScreen.gameObject.SetActive(true);
        ExecuteAfterDelay(10, () =>
        {
            endMiniGameScreen.gameObject.SetActive(false);
            _miniGameRunning = false;
            infos.text = DiceText;
            infos.gameObject.SetActive(true);
            _state = GameState.DiceRollPlayer1;
            backgroundGames.Stop();
            background.Play();
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
        startScreen.gameObject.SetActive(disp);
    }

    private void DisplayMiniGame(bool disp)
    {
        startMiniGameScreen.gameObject.SetActive(disp);
    }

    private void DisplayEndRound(bool disp)
    {
        TextMeshProUGUI text = endRoundScreen.GetComponentInChildren<TextMeshProUGUI>();
        text.text = String.Format("End of Round #{0}", _round);
        endRoundScreen.gameObject.SetActive(disp);
    }

    private void CheckForSpecialCells()
    {
        _specialMove = GameCell.CellPlane.GetCellOccupied() || GameCell.CellVolcano.GetCellOccupied() ||
            GameCell.CellRiver.GetCellOccupied();
    }

    private void OrderSpecialMove()
    {
        if (GameCell.CellRiver.GetCellOccupied())
        {
            if (_player1Tile == GameCell.CellRiver)
            {
                player1.SetSpecialMove(CelluloPlayer.SpecialMove.River, true);
                _player1Tile = GameCell.Cell5;
            }
            else
            {
                player2.SetSpecialMove(CelluloPlayer.SpecialMove.River, true);
                _player2Tile = GameCell.Cell5;
            }
        } 
        else if (GameCell.CellPlane.GetCellOccupied())
        {
            (_player1Tile, _player2Tile) = (_player2Tile, _player1Tile);

            player1.SetSpecialMove(CelluloPlayer.SpecialMove.Airplane, true, _player1Tile);
            player2.SetSpecialMove(CelluloPlayer.SpecialMove.Airplane, false, _player2Tile);
        } 
        else if (GameCell.CellVolcano.GetCellOccupied())
        {
            player1.SetSpecialMove(CelluloPlayer.SpecialMove.Volcano, true);
            player2.SetSpecialMove(CelluloPlayer.SpecialMove.Volcano, false);
        }
    }

    public void EndSpecialMove()
    {
        _isInSpecialMove = false;
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
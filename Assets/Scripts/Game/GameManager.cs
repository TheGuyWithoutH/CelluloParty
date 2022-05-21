using System;
using System.Collections;
using System.Collections.Generic;
using Game;
using Game.Cellulos;
using Game.Mini_Games;
using UnityEngine;

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

    public Camera mainCamera;
    public Camera player1Camera;
    public Camera player2Camera;

    public void Awake()
    {
        EnableCamera(CameraView.MainCamera);
    }

    // Start is called before the first frame update
    void Start()
    {
        _player1Tile = 0;
        _player2Tile = 0;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void EnableCamera(CameraView camera)
    {
        switch (camera)
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
        }
    }

    public void MiniGameQuit()
    {
        
    }

    private enum CameraView
    {
        MainCamera,
        Player1,
        Player2,
    }
    
}

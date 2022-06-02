using System;
using Game.Map;
using UnityEngine;

namespace Game.Cellulos
{
    public class CelluloPlayer : AgentBehaviour
    {
        public CelluloAgent player;
        
        private bool _isReady = false;
        private bool _isActive = true;
        private GameCell _cell; //position on the map
        private int _score;
        private bool _isTouch;
        private int _key;
        private Vector3 _move;
        private GameCell _targetCell;

        void Start()
        { 
            _score = 0;
            _cell = GameCell.Cell1;
        }

        void Update()
        {
            //update the position in function of the outcome of the dice throw
            if (_isActive)
            {
                MoveToTarget();
                
            }
            //update the score in function of the outcome of the game
        }

        private void MoveToTarget()
        {
            Debug.Log("moving1...");
            if (_isActive)
            {
                Debug.Log("moving2...");
                Debug.Log(_cell);
                Debug.Log(_targetCell);
                if (_cell != _targetCell)
                {
                    Debug.Log("moving3...");
                    if ((transform.position - Map.GameCells.GetCellPosition(_cell)).magnitude <= 0.3)
                        {
                        Debug.Log("next cell");
                            ++_cell;
                            Vector3 pos = _cell.GetCellOccupied() ? Map.GameCells.GetCellPosition(_cell) : _cell.GetCellShiftedPosition();
                            player.SetGoalPosition(pos.x, pos.z, 1);
                        }


                    if ((transform.position - Map.GameCells.GetCellShiftedPosition(_cell)).magnitude <= 0.3)
                        {
                        Debug.Log("next cell");
                        ++_cell;
                            Vector3 pos = _cell.GetCellOccupied() ? Map.GameCells.GetCellPosition(_cell) : _cell.GetCellShiftedPosition();
                            player.SetGoalPosition(pos.x, pos.z, 1);
                        }
                    
                }
            }
        }

        public void SetTargetCell(GameCell a)
        {
            Debug.Log(a);
            Debug.Log("cell Update");
            _targetCell = a;
        }

        /**
         * Methode that put the Cellulo player into his cell after Mini-games finishes
         */
        public void GoBackInCell()
        {
            player.isMoved = false;
            Vector3 pos = _cell.GetCellOccupied() ? Map.GameCells.GetCellPosition(_cell) : _cell.GetCellShiftedPosition();
            player.SetGoalPosition(pos.x, pos.z, 1);
        }

        /**
         * Returns true if the movement of the Cellulo to its target cell is done
         */
        public bool MoveIsDone()
        {
            //TODO: implement this
            throw new NotImplementedException();
        }

        public override void OnCelluloLongTouch(int key)
        {
            base.OnCelluloLongTouch(key);
            _isReady = true;
            _key = key;
        }

        public override void OnCelluloTouchBegan(int key)
        {
            base.OnCelluloTouchBegan(key);
            _isTouch = true;
            _key = key;
        }

        public override void OnCelluloTouchReleased(int key)
        {
            base.OnCelluloTouchReleased(key);
            _isTouch = false;
        }
        
        public bool IsReady => _isReady;

        public void SetNotReady()
        {
            _isReady = false;
        }

        public void SteeringReactivate()
        {
            _isActive = true;
        }
        
        public void SteeringDesactivate()
        {
            _isActive = false;
        }

        public int Score
        {
            get => _score;
            set => _score = value;
        }

        public bool IsTouch => _isTouch;

        public int Key
        {
            get => _key;
            set => _key = value;
        }
    }
}
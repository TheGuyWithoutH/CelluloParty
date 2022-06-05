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
        private bool _moveDone;

        void Start()
        { 
            _score = 0;
            _cell = GameCell.Cell1;
            _moveDone = false;
        }

        void Update()
        {
            //update the position in function of the outcome of the dice throw
            //update the score in function of the outcome of the game
        }

        private void MoveToTarget()
        {
            if (_isActive)
            {
                if (_cell != _targetCell)
                {
                    if (_cell < _targetCell) ++_cell;
                    else --_cell;
                    Vector3 pos = _cell.GetCellOccupied() ? _cell.GetCellShiftedPosition() : _cell.GetCellPosition();
                    Debug.Log("Cell : " + _cell + " " + pos);
                    player.SetGoalPosition(pos.x, pos.z, 1);
                }
            }
        }

        public void SetTargetCell(GameCell a)
        {
            Debug.Log(a);
            Debug.Log("cell Update");
            _targetCell = a;
            _moveDone = false;
            MoveToTarget();
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
            if (_moveDone)
            {
                _moveDone = false;
                return true;
            }

            return false;
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

        public override void OnCelluloGoalPoseReached()
        {
            base.OnCelluloGoalPoseReached();
            if (_cell == _targetCell) _moveDone = true;
            MoveToTarget();
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
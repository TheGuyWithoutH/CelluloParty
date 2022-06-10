using System;
using Game.Map;
using UnityEngine;

namespace Game.Cellulos
{
    public class CelluloPlayer : AgentBehaviour
    {
        public CelluloAgent celluloAgent;
        
        private bool _isReady = false;
        private bool _isActive = true;
        private GameCell _cell; //position on the map
        private int _score;
        private bool _isTouch;
        private int _key;
        private Vector3 _move;
        private GameCell _targetCell;
        private bool _moveDone;
        private bool _specialMove;
        public string playerName;
        
        public GameManager gameManager;
        private bool _notify;

        void Start()
        { 
            _score = 0;
            _cell = GameCell.Cell1;
            _moveDone = false;
            _specialMove = false;
            _notify = false;
        }

        private void MoveToTarget()
        {
            if (_isActive)
            {
                if (_cell != _targetCell )//&& _cell > GameCell.Cell1 && _cell < GameCell.Cell40)
                {
                    _cell.SetCellOccupied(false);
                    if (_cell < _targetCell) ++_cell;
                    else --_cell;
                    Vector3 pos = _cell.GetCellOccupied() ? _cell.GetCellShiftedPosition() : _cell.GetCellPosition();
                    Debug.Log("Cell : " + _cell + " " + pos);
                    _cell.SetCellOccupied(true);
                    celluloAgent.SetGoalPosition(pos.x, pos.z, 1);
                }
                // else if (_cell != _targetCell)
                // {
                //     _moveDone = true;
                // }
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
            celluloAgent.isMoved = false;
            Vector3 pos = _cell.GetCellOccupied() ? Map.GameCells.GetCellPosition(_cell) : _cell.GetCellShiftedPosition();
            celluloAgent.SetGoalPosition(pos.x, pos.z, 1);
        }

        public void SetSpecialMove(SpecialMove move, bool notify, GameCell cell = GameCell.Cell1)
        {
            _notify = notify;
            switch (move)
            {
                case SpecialMove.River:
                    _targetCell = GameCell.Cell5;
                    _moveDone = false;
                    MoveToTarget();
                    break;
                case SpecialMove.Volcano:
                    _targetCell = _cell - 3 < GameCell.Cell1 ? GameCell.Cell1 : _cell - 3;
                    _moveDone = false;
                    MoveToTarget();
                    break;
                case SpecialMove.Airplane:
                    _specialMove = true;
                    _targetCell = cell;
                    Vector3 pos = _targetCell.GetCellPosition();
                    celluloAgent.SetGoalPosition(pos.x, pos.z, 1);
                    break;
            }
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
            _isReady = false;
        }

        public override void OnCelluloGoalPoseReached()
        {
            base.OnCelluloGoalPoseReached();
            if (!_specialMove)
            {
                if (_cell == _targetCell)
                {
                    if (_specialMove && _notify)
                    {
                        _notify = false;
                        gameManager.EndSpecialMove();
                    }
                    else
                    {
                        _moveDone = true;
                    }
                }
                MoveToTarget();
            }
            else
            {
                _specialMove = false;
                _cell = _targetCell;
                if (_notify)
                {
                    _notify = false;
                    gameManager.EndSpecialMove();
                }
            }
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

        public bool getOneTouch()
        {
            if (_isTouch)
            {
                _isTouch = false;
                return true;
            }

            return false;
        }

        public int Key
        {
            get => _key;
            set => _key = value;
        }
        
        public enum SpecialMove
        {
            River,
            Volcano,
            Airplane,
        }
    }
}
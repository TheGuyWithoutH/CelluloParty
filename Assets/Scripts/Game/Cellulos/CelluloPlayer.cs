using System;
using Game.Map;
using UnityEngine;

namespace Game.Cellulos
{
    public class CelluloPlayer : AgentBehaviour
    {
        public CelluloAgent player;
        //Félix veut qu'on puisse gérer les texts à partir d'ici, j'ai pas très bien compris mais chill
        
        private bool _isReady = false;
        private bool _isActive = true;
        private Map.GameCell _cell; //position on the map
        private int _score;
        private bool _isTouch;
        private int _key;
        private Vector3 _move;
        private Map.GameCell _targetCell;

        void Start()
        { 
            _score = 0;
            _cell = GameCell.Cell1;
        }

        void Update()
        {
            //update the position in function of the outcome of the dice throw
            MoveToTarget();
            //update the score in function of the outcome of the game
        }

        private void MoveToTarget()
        {
            if (_isActive)
            {
                while (_cell != _targetCell)
                {

                    if (Enum.IsDefined(typeof(Map.GameCell), _cell))
                    {

                        if ((transform.position - Map.GameCells.GetCellPosition(_cell)).magnitude <= 0.1)
                        {
                            _cell++;
                            Vector3 pos = _cell.GetCellOccupied() ? Map.GameCells.GetCellPosition(_cell) : _cell.GetCellShiftedPosition();
                            player.SetGoalPosition(pos.x, pos.z, 2);
                        }


                        if ((transform.position - Map.GameCells.GetCellShiftedPosition(_cell)).magnitude <= 0.1)
                        {
                            _cell++;
                            Vector3 pos = _cell.GetCellOccupied() ? Map.GameCells.GetCellPosition(_cell) : _cell.GetCellShiftedPosition();
                            player.SetGoalPosition(pos.x, pos.z, 2);
                        }
                    }
                }
            }
        }

        public void SetTargetCell(Map.GameCell a)
        {
            _targetCell = a;
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
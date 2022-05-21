using System;
using UnityEngine;

namespace Game.Cellulos
{
    public class CelluloPlayer : AgentBehaviour
    {
        public CelluloAgent player;
        //Félix veut qu'on puisse gérer les texts à partir d'ici, j'ai pas très bien compris mais chill
        
        private bool _isReady = false;
        private bool _isActive = false;
        private int _cell; //position on the map
        private int _score;
        private bool _isTouch;

        void Start()
        { 
            _score = 0;
            _cell = 0;
        }

        void Update()
        {
            //update the position in function of the outcome of the dice throw
            //update the score in function of the outcome of the game
        }
        
        public override void OnCelluloLongTouch(int key)
        {
            base.OnCelluloLongTouch(key);
            _isReady = true;
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

        public int Score => _score;

        public bool IsTouch => _isTouch;
    }
}
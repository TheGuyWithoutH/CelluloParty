using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game.Dices
{
    public abstract class Dice : MonoBehaviour
    {
        private Rigidbody _rb;
        public Vector3 diceVelocity;
        protected bool _done;
        protected int _result;
        
        void Awake () {
            _rb = GetComponent<Rigidbody> ();
            Debug.Log("enter here");
            _done = false;
            _result = -1;
        }

        private void Update()
        {
            diceVelocity = _rb.velocity;
        }

        public void ThrowDice()
        {
            _done = false;
            _result = -1;
            float dirX = Random.Range(100, 800);
            float dirY = Random.Range(100, 800);
            float dirZ = Random.Range(100, 800);
            transform.position = new Vector3 (9.5f, 9f, -9f);
            transform.rotation = Quaternion.identity;
            _rb.AddForce (transform.up * 500);
            _rb.AddTorque (dirX, dirY, dirZ);
        }

        public bool DiceThrowDone()
        {
            return _done && _result > 0;
        }

        public int GetDiceScore()
        {
            if (_result <= 0) throw new Exception("Dice not working");
            return _result;
        }

        public abstract void SetDiceResult(DiceFace face);
    }

    public enum DiceFace
    {
        Face1,
        Face2,
        Face3,
        Face4,
        Face5,
        Face6
    }
}
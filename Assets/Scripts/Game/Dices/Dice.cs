using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game.Dices
{
    public abstract class Dice : MonoBehaviour
    {
        static Rigidbody rb;
        public Vector3 diceVelocity;
        protected bool _done;
        protected int _result;
        
        void Start () {
            rb = GetComponent<Rigidbody> ();
            _done = false;
            _result = -1;
        }

        private void Update()
        {
            diceVelocity = rb.velocity;
        }

        public void ThrowDice()
        {
            _done = false;
            _result = -1;
            float dirX = Random.Range (0, 500);
            float dirY = Random.Range (0, 500);
            float dirZ = Random.Range (0, 500);
            transform.position = new Vector3 (0, 2, 0);
            transform.rotation = Quaternion.identity;
            rb.AddForce (transform.up * 500);
            rb.AddTorque (dirX, dirY, dirZ);
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
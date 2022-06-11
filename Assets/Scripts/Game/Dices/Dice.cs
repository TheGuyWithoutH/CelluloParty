using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game.Dices
{
    public abstract class Dice : MonoBehaviour
    {
        private Rigidbody _rb;
        public Vector3 diceVelocity;
        public AudioSource effect;
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
            float rotX = Random.Range(0, 180);
            float rotY = Random.Range(0, 180);
            float rotZ = Random.Range(0, 180);
            float dirX = Random.Range(-3000, 3000);
            float dirY = Random.Range(-3000, 3000);
            float dirZ = Random.Range(-3000, 3000);
            transform.position = new Vector3 (9.5f, 9f, -9f);
            transform.rotation = Quaternion.Euler(new Vector3(rotX, rotY, rotZ));

            _rb.AddForce (transform.up * 200);
            _rb.AddTorque (dirX, dirY, dirZ);
            effect.Play();
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
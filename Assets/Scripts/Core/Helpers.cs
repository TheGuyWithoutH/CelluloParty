using System;
using System.Collections;
using UnityEngine;

namespace Core
{
    public class Helpers: MonoBehaviour
    {
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

        public void ExecuteAfterDelay(float time, Action task)
        {
            StartCoroutine(ExecuteAfterTime(0.5f, task));
        }
    }
}
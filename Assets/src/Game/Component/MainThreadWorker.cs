using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Component
{
    public class MainThreadWorker : MonoBehaviour
    {
        private readonly Queue<Action> _jobs = new Queue<Action>();

        public MainThreadWorker AddTask(Action action)
        {
            _jobs.Enqueue(action);
            
            return this;
        }
        
        private void Update()
        {
            while (_jobs.Count > 0) _jobs.Dequeue().Invoke();
        }
    }
}
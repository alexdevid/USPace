using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

namespace Game
{
    public class Promise
    {
        private readonly UnityEvent _onFinish = new UnityEvent();
        private readonly UnityEvent _onException = new UnityEvent();

        private readonly UnityAction _action;
        
        public Promise(UnityAction action)
        {
            _action = action;
            Run();
        }

        public Promise Then(UnityAction action)
        {
            _onFinish.AddListener(action);

            return this;
        }

        public Promise Catch(UnityAction action)
        {
            _onException.AddListener(action);

            return this;
        }
        
        private Task<bool> Run()
        {
            return Task.Run(() =>
            {
                try
                {
                    _action.Invoke();
                    _onFinish.Invoke();
                }
                catch (Exception e)
                {
                    _onException.Invoke();
                }

                return true;
            });
        }
    }
}
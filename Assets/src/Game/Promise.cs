using System;
using System.Threading.Tasks;
using UnityEngine;

namespace Game
{
    //lalala
    public class Promise : Task
    {
        public Promise(Action action) : base(action)
        {
            Promise p = new Promise(() =>
            {
                Debug.Log("Go");
            }).Then(response =>
            {
                Debug.Log("response: " + response);
            }).Catch(e =>
            {
                Debug.Log("Error: " + e.Message);
            });
        }

        public Promise Then(Action<string> action)
        {
            return this;
        }

        public Promise Catch(Action<Exception> action)
        {
            return this;
        }
    }
}
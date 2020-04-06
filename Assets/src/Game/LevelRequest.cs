using System;
using System.Threading.Tasks;
using Model;
using Network.DataTransfer;
using Network.DataTransfer.World;
using UnityEngine;
using UnityEngine.Events;

namespace Game
{
    public class LevelRequest
    {
        private const string ListMethod = "world.list";
        private const string GetMethod = "world.get";
        
        private UnityAction<Exception> _exceptionHandler;
        private Level _level;
        
        public LevelRequest Then(UnityAction<Level> response)
        {
            GameController.AddTask(async () =>
            {
                try
                {
                    Level level = await Send();
                    response.Invoke(level);
                }
                catch (Exception e)
                {
                    _exceptionHandler.Invoke(e);
                }
            });
            
            return this;
        }

        public LevelRequest Catch(UnityAction<Exception> action)
        {
            _exceptionHandler = action;
            
            return this;
        }
        
        private async Task<Level> Send()
        {
            Request<WorldRequest> request = new Request<WorldRequest>(GetMethod, new WorldRequest(1));
            try
            {
                string json = await request.Send();
                
                WorldResponse response = JsonUtility.FromJson<WorldResponse>(json);
                    
                return Level.CreateFromDTO(response);
            }
            catch (Exception e)
            {
                _exceptionHandler.Invoke(e);
                
                return null;
            }
        }
    }
}
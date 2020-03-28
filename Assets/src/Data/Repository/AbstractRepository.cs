using System.Collections.Generic;
using Model;
using UnityPackages;

namespace Data.Repository
{
    public class AbstractRepository
    {
        protected static T Find<T>(int id) where T : StorageObject
        {
            return Game.App.Storage.Get<T>(id);
        }

        protected static List<T> FindAll<T>() where T : StorageObject
        {
            return Game.App.Storage.GetAll<T>();
        }

        protected static Promise<List<T>> FindAsync<T>() where T : StorageObject
        {
            return new Promise<List<T>>((resolve, reject) =>
            {
                var models = FindAll<T>();
                
                resolve(models);
            });
        }
    }
}
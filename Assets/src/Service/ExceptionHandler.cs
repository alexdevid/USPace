using System;
using UnityEngine;

namespace Service
{
    public static class ExceptionHandler
    {
        public static void Handle(Exception e)
        {
            Debug.LogError(e.Message);
        }
    }
}
using System;
using UnityEngine;

namespace FluffyDisket
{
    public class CustomSingleton<T> : MonoBehaviour
    {
        private static T Instance;
        public static T GetInstance() => Instance;

        public static bool ExistInstance() => Instance != null;

        protected virtual void Awake()
        {
            if (Instance == null)
            {
                Instance = GetComponent<T>();
                DontDestroyOnLoad(this.gameObject);
            }
            else
            {
                this.enabled = false;
            }
        }
    }
}
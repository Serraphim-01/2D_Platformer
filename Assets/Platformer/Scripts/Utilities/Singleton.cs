using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Spatialminds.Platformer
{
    public abstract class Singleton<T> : MonoBehaviour
    {
        private static T _instance;

        public static T instance
        {
            get
            {
                if(Equals(_instance, null) || _instance == null || _instance.Equals(null))
                {
                    var instanceObject = FindObjectOfType<Singleton<T>>();
                    _instance = instanceObject.GetComponent<T>();
                }

                return _instance;
            }
            set
            {
                _instance = value;
            }
        }

        /// <summary>
        /// Call this method in the concrete class
        /// </summary>
        /// <param name="singletonInstance"></param>
        protected void InitializeSingleton(T singletonInstance)
        {
            var instanceObject = FindObjectsOfType<Singleton<T>>();
            if(instanceObject.Length>1)
            {
                Destroy(this.gameObject);
                return;
            }

            if(_instance == null)
            {
                _instance = singletonInstance;
            }else if (_instance.Equals(singletonInstance))
            {
                Debug.LogWarning("Found two singletons of the type "+this);
                Destroy(this.gameObject);
            }
        }
    }
}

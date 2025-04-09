using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Spatialminds.Platformer
{
    public class ServiceLocator : Singleton<ServiceLocator>
    {
        private Dictionary<Type, MonoBehaviour> services;

        void Awake()
        {
            InitializeSingleton(this);
            services = new Dictionary<Type, MonoBehaviour>();
        }

        public T GetService<T>() where T:MonoBehaviour, new()
        {
            UnityEngine.Assertions.Assert.IsNotNull(services, "Service has been requested prior to locator initialization");

            //locate the service
            bool serviceLocated = services.ContainsKey(typeof(T));

            if(!serviceLocated)
            {
                services.Add(typeof(T), FindObjectOfType<T>());
            }

            UnityEngine.Assertions.Assert.IsTrue(services.ContainsKey(typeof(T)), "Cannot find the service "+typeof(T).ToString());

            var service = (T)services[typeof(T)];
            
            UnityEngine.Assertions.Assert.IsNotNull(service, "Service is unavailable - "+typeof(T).ToString() );

            return service;
            
        }
    }
}

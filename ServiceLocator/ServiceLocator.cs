using System;
using System.Collections.Generic;
using UnityEngine;

namespace Utils {
    public class ServiceLocator {

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void Initialize() {
            var services = RegisterService.GetRegisteredServices();

            foreach (var service in services) {
                var instance = (IService)Activator.CreateInstance(service.serviceClass);
                Register(service.type, instance);
            }
        }

        private static readonly Dictionary<Type, IService> _services = new Dictionary<Type, IService>();

        public static void Register<T>(T service) where T : IService {
            Type type = typeof(T);
            Register(type, service);
        }

        public static void Register<T>(Type type, T service) where T : IService {
            if (_services.ContainsKey(type)) {
                throw new ServiceLocatorException($"Service {type} already registered.");
            }

            _services.Add(type, service);
        }

        public static void Unregister<T>() where T : IService {
            Type type = typeof(T);

            if (!_services.ContainsKey(type)) {
                throw new ServiceLocatorException($"Service {type} is not registered.");
            }

            _services.Remove(type);
        }

        public static T Get<T>() where T : IService {
            Type type = typeof(T);

            if (!_services.ContainsKey(type)) {
                throw new ServiceLocatorException($"Service {type} is not registered.");
            }

            return (T)_services[type];
        }
    }

    public class ServiceLocatorException : Exception {
        public ServiceLocatorException(string message) : base(message) { }
    }
}


using System;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;

namespace Utils {
    [AttributeUsage(AttributeTargets.Class)]
    public class RegisterService : Attribute {
        private Type _serviceType;

        public RegisterService(Type type) {
            _serviceType = type;
        }

        public Type GetServiceType() {
            return _serviceType;
        }

        public static IEnumerable<(Type type, Type serviceClass)> GetRegisteredServices() {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();

            var result = new ArrayList();
            foreach (var assembly in assemblies) {
                var types = assembly.GetTypes();

                foreach (var type in types) {
                    var attribute = type.GetCustomAttribute<RegisterService>();
                    if (attribute == null) continue;
                    result.Add((attribute.GetServiceType(), type));
                }
            }

            return ((Type type, Type serviceClass)[])result.ToArray(typeof((Type type, Type serviceClass)));
        }
    }
}


using System;
using System.Collections.Generic;

namespace Services
{
    public class AllServices
    {
        private static AllServices _instance;
        public static AllServices Container => _instance ??= new AllServices();

        private Dictionary<Type, IService> _dictionary = new Dictionary<Type, IService>();

        public void Register<T>(T service) where T : IService
        {
            _dictionary.Add(typeof(T), service);
        }

        public T Get<T>() where T : IService
        {
            return (T)_dictionary[typeof(T)];
        }
    }
}
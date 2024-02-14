using Services;
using UnityEngine;

namespace Factories
{
    public interface IGameFactory : IService

    {
        public T Create<T>(string path) where T : MonoBehaviour;
        public T Create<T>(string path, Vector3 pos) where T : MonoBehaviour;
    }
}
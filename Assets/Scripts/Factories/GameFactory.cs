using UnityEngine;

namespace Factories
{
    public class GameFactory : IGameFactory
    {
        public T Create<T>(string path) where T : MonoBehaviour
        {
            return Object.Instantiate(Resources.Load<T>(path));
        }

        public T Create<T>(string path, Vector3 pos) where T : MonoBehaviour
        {
            return Object.Instantiate(Resources.Load<T>(path), pos, Quaternion.identity);
        }
    }
}
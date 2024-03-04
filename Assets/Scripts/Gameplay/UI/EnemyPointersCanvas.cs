using Services;
using Services.Factories;
using UnityEngine;

namespace Gameplay.UI
{
    public class EnemyPointersCanvas : MonoBehaviour
    {
        public EnemyPointerImage CreateEnemyPointer()
        {
            return AllServices.Container.Get<IGameFactory>().CreateEnemyPointerImage(transform);
        }
    }
}
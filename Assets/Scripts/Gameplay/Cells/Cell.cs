using Gameplay.Units.Creatures;
using UnityEngine;

namespace Gameplay.Cells
{
    [RequireComponent(typeof(Collider))]
    public class Cell : MonoBehaviour
    {
        [SerializeField] private Transform _placeForCreature;
        public Creature currentCreature;
        public Vector3 GetPosition => _placeForCreature.position;
    }
}
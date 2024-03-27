using Gameplay.MergeEntities;
using UnityEngine;

namespace Gameplay.Cells
{
    [RequireComponent(typeof(Collider))]
    public class Cell : MonoBehaviour
    {
        [SerializeField] private Transform _placeForCreature;
        public MergeEntity currentMergeEntity;
        public Vector3 GetPosition => _placeForCreature.position;
    }
}
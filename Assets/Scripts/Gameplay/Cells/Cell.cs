using Gameplay.MergeEntities;
using UnityEngine;
using UnityEngine.Serialization;

namespace Gameplay.Cells
{
    [RequireComponent(typeof(Collider))]
    public class Cell : MonoBehaviour
    {
        [SerializeField] private Transform _placeForCreature;
        [FormerlySerializedAs("currentCreature")] public MergeEntity currentMergeEntity;
        public Vector3 GetPosition => _placeForCreature.position;
    }
}
using UnityEngine;

namespace Gameplay.Creatures
{
    [CreateAssetMenu(fileName = "CreatureDescription", menuName = "CreatureDescription")]
    public class CreatureDescription : ScriptableObject
    {
        [SerializeField] Creature _creaturePrefab;
        [SerializeField] int _level;
        [SerializeField] int _damage;
        [SerializeField] int _maxHealth;
        [SerializeField] float _moveSpeed;

        public Creature CreaturePrefab => _creaturePrefab;
        public int Level => _level;
        public int Damage => _damage;
        public int MaxHealth => _maxHealth;
        public float MoveSpeed => _moveSpeed;
    }
}
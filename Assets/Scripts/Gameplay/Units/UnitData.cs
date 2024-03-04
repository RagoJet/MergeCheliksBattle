using UnityEngine;

namespace Gameplay.Units
{
    [CreateAssetMenu(fileName = "UnitData", menuName = "UnitData")]
    public class UnitData : ScriptableObject
    {
        [SerializeField] Unit _unitPrefab;
        [SerializeField] int _level;
        [SerializeField] int _damage;
        [SerializeField] int _maxHealth;
        [SerializeField] int _moneyFromDeath;

        public Unit UnitPrefab => _unitPrefab;
        public int Level => _level;
        public int Damage => _damage;
        public int MaxHealth => _maxHealth;
        public int MoneyFromDeath => _moneyFromDeath;
    }
}
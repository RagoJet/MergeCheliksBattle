using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Gameplay.Units.Creatures
{
    [CreateAssetMenu(fileName = "CreatureDescriptions", menuName = "CreatureDescriptions")]
    public class CreatureDescriptions : ScriptableObject

    {
        [SerializeField] private List<UnitData> _listOfMelee;
        [SerializeField] private List<UnitData> _listOfRange;

        private void OnValidate()
        {
            _listOfMelee = _listOfMelee.OrderBy(x => x.Level).ToList();
            _listOfRange = _listOfRange.OrderBy(x => x.Level).ToList();
        }

        public UnitData GetMeleeUnitData(int level)
        {
            return _listOfMelee[level];
        }

        public UnitData GetRangeUnitData(int level)
        {
            return _listOfRange[level];
        }
    }
}
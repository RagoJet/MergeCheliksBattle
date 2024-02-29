using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Gameplay.Units.Creatures
{
    [CreateAssetMenu(fileName = "CreatureDescriptions", menuName = "CreatureDescriptions")]
    public class CreatureDescriptions : ScriptableObject

    {
        [SerializeField] private List<UnitData> _listOfDescriptions;

        private void OnValidate()
        {
            _listOfDescriptions = _listOfDescriptions.OrderBy(x => x.Level).ToList();
        }

        public UnitData GetCreatureData(int level)
        {
            return _listOfDescriptions[level];
        }
    }
}
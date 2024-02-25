using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Gameplay.Units.Creatures
{
    [CreateAssetMenu(fileName = "CreatureDescriptions", menuName = "CreatureDescriptions")]
    public class CreatureDescriptions : ScriptableObject
    {
        [SerializeField] private List<CreatureDescription> _listOfCreatureDescriptions;

        private void OnValidate()
        {
            _listOfCreatureDescriptions = _listOfCreatureDescriptions.OrderBy(x => x.Level).ToList();
        }

        public CreatureDescription GetCreatureData(int level)
        {
            return _listOfCreatureDescriptions[level];
        }
    }
}
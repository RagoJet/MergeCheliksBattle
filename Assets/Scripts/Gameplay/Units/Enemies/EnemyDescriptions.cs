using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Gameplay.Units.Enemies
{
    [CreateAssetMenu(fileName = "EnemyDescriptions", menuName = "EnemyDescriptions")]
    public class EnemyDescriptions : ScriptableObject
    {
        [SerializeField] private List<UnitData> _elvesList;
        [SerializeField] private List<UnitData> _undeadList;
        [SerializeField] private List<UnitData> _orcsList;

        private void OnValidate()
        {
            _elvesList = _elvesList.OrderBy(x => x.Level).ToList();
            _undeadList = _undeadList.OrderBy(x => x.Level).ToList();
            _orcsList = _orcsList.OrderBy(x => x.Level).ToList();
        }

        public UnitData GetElvesData(int level)
        {
            return _elvesList[level];
        }

        public UnitData GetUndeadData(int level)
        {
            return _undeadList[level];
        }

        public UnitData GetOrcData(int level)
        {
            return _orcsList[level];
        }
    }
}
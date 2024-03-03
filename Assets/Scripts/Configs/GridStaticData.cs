using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(fileName = "GridStaticData", menuName = "GridStaticData")]
    public class GridStaticData : ScriptableObject
    {
        [SerializeField] private int _rows;
        [SerializeField] private int _collumns;
        [SerializeField] private float _offset;

        public int Rows => _rows;
        public int Collumns => _collumns;
        public float Offset => _offset;
    }
}
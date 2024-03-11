using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(fileName = "SoundsData", menuName = "SoundsData")]
    public class SoundsData : ScriptableObject
    {
        public AudioClip[] gameplayMusics;
        public AudioClip winClip;
        public AudioClip loseClip;
        public AudioClip startBattleClip;
        public AudioClip enemyKilledClip;
        public AudioClip mergeClip;
        public AudioClip pickUpCreatureClip;
        public AudioClip downCreatureClip;
        public AudioClip attackClip;
        public AudioClip buyCreatureClip;
    }
}
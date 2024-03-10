using UnityEngine;
using UnityEngine.Serialization;

namespace Configs
{
    [CreateAssetMenu(fileName = "SoundsData", menuName = "SoundsData")]
    public class SoundsData : ScriptableObject
    {
        public AudioClip[] gameplayMusics;
        public AudioClip winClip;
        public AudioClip loseClip;
        public AudioClip startFightClip;
        public AudioClip enemyKilledClip;
        public AudioClip mergeClip;
        public AudioClip pickUpCreatureClip;
    }
}
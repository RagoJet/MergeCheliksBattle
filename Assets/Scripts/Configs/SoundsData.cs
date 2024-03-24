using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(fileName = "SoundsData", menuName = "SoundsData")]
    public class SoundsData : ScriptableObject
    {
        public AudioClip[] gameplayMusics;
        public AudioClip startBattleClip;

        public AudioClip winClip;
        public AudioClip loseClip;

        public AudioClip mergeClip;
        public AudioClip pickUpMergeEntityClip;
        public AudioClip downMergeEntityClip;

        public AudioClip attackClip;

        public AudioClip getGoldClip;
        public AudioClip buyClip;
        public AudioClip noGoldClip;
        public AudioClip pressButtonClip;
    }
}
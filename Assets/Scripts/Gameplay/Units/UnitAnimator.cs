using UnityEngine;

namespace Gameplay.Units
{
    [RequireComponent(typeof(Animator))]
    public class UnitAnimator : MonoBehaviour
    {
        private Animator _animator;
        private static readonly int NoBusy = Animator.StringToHash("NoBusy");
        private static readonly int Attack = Animator.StringToHash("Attack");
        private static readonly int Death = Animator.StringToHash("Death");
        private static readonly int Speed = Animator.StringToHash("Speed");

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        public void SetDataOfVelocity(float value)
        {
            _animator.SetFloat(Speed, value);
        }
    }
}
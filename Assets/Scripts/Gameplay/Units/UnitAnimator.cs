using UnityEngine;

namespace Gameplay.Units
{
    [RequireComponent(typeof(Animator))]
    public class UnitAnimator : MonoBehaviour
    {
        private Animator _animator;
        private static readonly int Freedom = Animator.StringToHash("Freedom");
        private static readonly int Attack = Animator.StringToHash("Attack");
        private static readonly int Death = Animator.StringToHash("Death");
        private static readonly int Speed = Animator.StringToHash("Speed");

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            Health health = GetComponent<Health>();
            health.OnDie += SetDeathTrigger;
        }

        public void SetDataOfVelocity(float value)
        {
            _animator.SetFloat(Speed, value);
        }

        public void SetDeathTrigger()
        {
            _animator.SetTrigger(Death);
        }

        public void SetAttackTrigger()
        {
            _animator.SetTrigger(Attack);
        }

        public void SetFreeTrigger()
        {
            _animator.ResetTrigger(Attack);
            _animator.SetTrigger(Freedom);
        }
    }
}
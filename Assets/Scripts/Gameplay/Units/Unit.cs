using UnityEngine;
using UnityEngine.AI;

namespace Gameplay.Units
{
    [RequireComponent(typeof(UnitAnimator), typeof(NavMeshAgent))]
    public class Unit : MonoBehaviour
    {
        protected int _health;
        private NavMeshAgent _agent;
        private UnitAnimator _unitAnimator;

        private void Awake()
        {
            _unitAnimator = GetComponent<UnitAnimator>();
            _agent = GetComponent<NavMeshAgent>();
            _agent.enabled = false;
        }

        public void ReadyToBattle()
        {
            _agent.enabled = true;
        }

        public void GoTo(Vector3 pos)
        {
            _unitAnimator.SetDataOfVelocity(_agent.velocity.sqrMagnitude);
            if ((transform.position - pos).sqrMagnitude > 0.3f)
            {
                _agent.SetDestination(pos);
            }
        }

        public virtual void Refresh()
        {
        }
    }
}
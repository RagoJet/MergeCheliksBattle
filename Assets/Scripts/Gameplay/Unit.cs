using UnityEngine;
using UnityEngine.AI;

namespace Gameplay.Crowds
{
    [RequireComponent(typeof(Animator), typeof(NavMeshAgent))]
    public class Unit : MonoBehaviour
    {
        protected int _health;
        private Animator _animator;
        private NavMeshAgent _agent;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _agent = GetComponent<NavMeshAgent>();
            _agent.enabled = false;
        }

        public void ReadyToBattle()
        {
            _agent.enabled = true;
        }

        public void GoTo(Vector3 pos)
        {
            _agent.SetDestination(pos);
        }

        public virtual void Refresh()
        {
        }
    }
}
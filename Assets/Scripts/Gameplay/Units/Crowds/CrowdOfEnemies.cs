using Gameplay.Units.Crowds.CrowdOfUnitsState;
using Services;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

namespace Gameplay.Units.Crowds
{
    [RequireComponent(typeof(NavMeshAgent), typeof(EnemyCrowdPointer))]
    public class CrowdOfEnemies : CrowdOfUnits
    {
        private NavMeshAgent _agent;
        private float _timeFromLastMove;

        private Vector3 _startPos;

        private void Start()
        {
            _startPos = transform.position;
            _agent = GetComponent<NavMeshAgent>();

            VagrancyCrowdState vagrancyCrowdState = new VagrancyCrowdState(Patrol);
            BattleCrowdState battleCrowdState = new BattleCrowdState(StopMoving, ChangePositionToUnits);

            _stateMachine.AddTransition(vagrancyCrowdState, battleCrowdState, () => fightMode);
            _stateMachine.AddTransition(battleCrowdState, vagrancyCrowdState, () => fightMode == false);
            _stateMachine.SetState(vagrancyCrowdState);

            void StopMoving()
            {
                _agent.SetDestination(transform.position);
            }
        }

        private void Update()
        {
            _stateMachine.Tick();
        }

        private void Patrol()
        {
            base.FormatUnits();
            if (Time.time - _timeFromLastMove >= 6f)
            {
                float x = Random.Range(-15, 15);
                float z = Random.Range(-15, 15);
                Vector3 pos = _startPos + new Vector3(x, 0, z);
                _agent.SetDestination(pos);
                _timeFromLastMove = Time.time;
            }
        }

        protected override void Die()
        {
            AllServices.Container.Get<EventBus>().OnDeathEnemyCrowd();
            base.Die();
        }
    }
}
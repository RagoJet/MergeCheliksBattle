using Gameplay.Units.Crowds.CrowdOfUnitsState;
using Services;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

namespace Gameplay.Units.Crowds
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class CrowdOfEnemies : CrowdOfUnits
    {
        private NavMeshAgent _agent;
        private float _timeFromLastMove;

        private void Start()
        {
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
            if (Time.time - _timeFromLastMove >= 7f)
            {
                float x = Random.Range(-70, 70);
                float z = Random.Range(-70, 70);
                _agent.SetDestination(new Vector3(x, transform.position.y, z));
                _timeFromLastMove = Time.time;
            }
        }

        protected override void Die()
        {
            AllServices.Container.Get<EventBus>().OnKilledEnemyCrowd();
            base.Die();
        }
    }
}
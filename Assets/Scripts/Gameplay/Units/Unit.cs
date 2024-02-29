using Gameplay.Units.Crowds;
using Gameplay.Units.UnitStates;
using States;
using UnityEngine;
using UnityEngine.AI;

namespace Gameplay.Units
{
    [RequireComponent(typeof(Health), typeof(UnitAnimator), typeof(NavMeshAgent))]
    public class Unit : MonoBehaviour
    {
        private NavMeshAgent _agent;
        private UnitAnimator _unitAnimator;

        protected UnitData _data;
        protected Health health;

        private bool _fightMode;
        private StateMachine _stateMachine = new StateMachine();
        private CrowdOfUnits _myCrowd;
        private Health _targetHealth;

        private void Awake()
        {
            health = GetComponent<Health>();
            health.OnDie += RemoveFromCrowd;
            health.OnDie += StopFight;

            _unitAnimator = GetComponent<UnitAnimator>();
            _agent = GetComponent<NavMeshAgent>();
            _agent.enabled = false;

            VagrancyUnitState vagrancyUnitState = new VagrancyUnitState();
            BattleUnitState battleUnitState = new BattleUnitState(this, _agent.stoppingDistance);

            _stateMachine.AddTransition(vagrancyUnitState, battleUnitState, () => _fightMode);
            _stateMachine.AddTransition(battleUnitState, vagrancyUnitState, () => _fightMode == false);

            _stateMachine.SetState(vagrancyUnitState);
        }

        public void SetData(UnitData data)
        {
            _data = data;
        }

        public void SetCrowd(CrowdOfUnits crowd)
        {
            _myCrowd = crowd;
        }

        private void RemoveFromCrowd()
        {
            _myCrowd.RemoveFromCrowd(this);
        }

        public void Refresh()
        {
            health.Refresh(_data.MaxHealth);
        }

        private void Update()
        {
            _stateMachine.Tick();
        }

        public Unit GetClosestOpponent()
        {
            Unit unit = _myCrowd.GetOpponentFromTargetCrowd(transform.position);
            if (unit == null)
            {
                _fightMode = false;
            }

            return unit;
        }

        public void StartFight()
        {
            _fightMode = true;
        }

        public void StopFight()
        {
            _fightMode = false;
        }


        public void SetTarget(Health target)
        {
            _targetHealth = target;
        }

        public void Attack()
        {
            if (_targetHealth != null)
            {
                if (_targetHealth.IsAlive)
                {
                    _targetHealth.TakeDamage(_data.Damage);
                }
            }
        }

        public void GoTo(Vector3 pos)
        {
            _unitAnimator.SetDataOfVelocity(_agent.velocity.sqrMagnitude);
            if ((transform.position - pos).sqrMagnitude > 0.3f)
            {
                _agent.SetDestination(pos);
            }
        }

        public void NavMeshAgentOn()
        {
            _agent.enabled = true;
        }
    }
}
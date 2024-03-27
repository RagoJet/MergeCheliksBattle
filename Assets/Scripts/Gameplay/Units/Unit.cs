using System.Collections;
using Gameplay.Units.Crowds;
using Gameplay.Units.UnitStates;
using Services;
using Services.Audio;
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

        private UnitData _data;
        private Health _health;

        private CrowdOfUnits _myCrowd;
        private Health _targetHealth;
        public Health TargetHealth => _targetHealth;
        private StateMachine _stateMachine = new StateMachine();
        private bool _fightMode;

        public bool IsRange => _data.RangeAttack > 2.5f;
        public int Level => _data.Level;

        public int Damage => _data.Damage;
        public int MaxHealth => _data.MaxHealth;

        private void Awake()
        {
            _health = GetComponent<Health>();
            _health.onDie += RemoveFromCrowd;
            _health.onDie += StopFight;
            _health.onDie += Dying;

            _unitAnimator = GetComponent<UnitAnimator>();
            _agent = GetComponent<NavMeshAgent>();
            NavMeshAgentOff();
            InitStateMachine();
            StopFight();
        }

        private void InitStateMachine()
        {
            VagrancyUnitState vagrancyUnitState = new VagrancyUnitState();
            FindTargetUnitState findTargetUnitState = new FindTargetUnitState(this);
            MovingToTargetUnitState movingToTargetUnitState = new MovingToTargetUnitState(this);
            AttackingUnitState attackingUnitState = new AttackingUnitState(this);

            _stateMachine.AddTransition(vagrancyUnitState, findTargetUnitState, () => _fightMode);
            _stateMachine.AddTransition(findTargetUnitState, movingToTargetUnitState, HasTarget);

            _stateMachine.AddTransition(movingToTargetUnitState, attackingUnitState, IsTargetNear);
            _stateMachine.AddTransition(movingToTargetUnitState, findTargetUnitState, () => HasTarget() == false);

            _stateMachine.AddTransition(attackingUnitState, movingToTargetUnitState, () => IsTargetNear() == false);
            _stateMachine.AddTransition(attackingUnitState, findTargetUnitState, () => HasTarget() == false);


            _stateMachine.AddAnyTransition(vagrancyUnitState, () => _fightMode == false);
            _stateMachine.SetState(vagrancyUnitState);


            bool HasTarget()
            {
                return _targetHealth != null && _targetHealth.IsAlive;
            }

            bool IsTargetNear()
            {
                float sqrDistance = (transform.position - _targetHealth.transform.position).sqrMagnitude;
                float _sqrRange = _agent.stoppingDistance * _agent.stoppingDistance;

                return sqrDistance <= _sqrRange;
            }
        }


        public void SetData(UnitData data)
        {
            _data = data;
        }

        public void SetCrowd(CrowdOfUnits crowd)
        {
            _myCrowd = crowd;
            _health.onTakeDamage += _myCrowd.GetComponent<InfoOfCrowd>().RemoveHealth;
        }

        private void RemoveFromCrowd()
        {
            _myCrowd.RemoveFromCrowd(this);
        }

        public void Refresh()
        {
            _health.Refresh(_data.MaxHealth);
        }

        private void Update()
        {
            _stateMachine.Tick();
            _unitAnimator.SetDataOfVelocity(_agent.velocity.sqrMagnitude);
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
            _agent.speed = _data.MoveSpeed;
            _agent.stoppingDistance = _data.RangeAttack;
            _fightMode = true;
        }

        public void StopFight()
        {
            _agent.speed = 10;
            _agent.stoppingDistance = 0.05f;
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
                    AllServices.Container.Get<IAudioService>().AttackSound();
                    _targetHealth.TakeDamage(_data.Damage);
                }
            }
        }

        public void GoTo(Vector3 pos)
        {
            _agent.SetDestination(pos);
        }

        public void NavMeshAgentOn()
        {
            _agent.enabled = true;
        }

        private void NavMeshAgentOff()
        {
            _agent.enabled = false;
        }

        private void Dying()
        {
            NavMeshAgentOff();
            _myCrowd.GetComponent<InfoOfCrowd>().RemoveDamage(_data.Damage);
            int money = _data.MoneyFromDeath;
            if (money > 0)
            {
                AllServices.Container.Get<EventBus>().OnDeathEnemy(transform, money);
            }

            StartCoroutine(ProcessOfDying());
        }

        private IEnumerator ProcessOfDying()
        {
            yield return new WaitForSeconds(3f);
            Destroy(this.gameObject);
        }
    }
}
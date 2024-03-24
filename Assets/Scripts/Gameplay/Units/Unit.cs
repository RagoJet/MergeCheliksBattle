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

        protected UnitData _data;
        protected Health health;

        private bool _fightMode;
        private StateMachine _stateMachine = new StateMachine();
        private CrowdOfUnits _myCrowd;

        private Health _targetHealth;
        
        public bool IsRange => _data.RangeAttack > 2.5f;
        public int Level => _data.Level;

        public int Damage => _data.Damage;
        public int MaxHealth => _data.MaxHealth;

        private void Awake()
        {
            health = GetComponent<Health>();
            health.onDie += RemoveFromCrowd;
            health.onDie += StopFight;
            health.onDie += Dying;

            _unitAnimator = GetComponent<UnitAnimator>();
            _agent = GetComponent<NavMeshAgent>();
            NavMeshAgentOff();
            InitStateMachine();
            StopFight();
        }

        private void InitStateMachine()
        {
            VagrancyUnitState vagrancyUnitState = new VagrancyUnitState();
            BattleUnitState battleUnitState = new BattleUnitState(this);

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
            health.onTakeDamage += _myCrowd.GetComponent<InfoOfCrowd>().RemoveHealth;
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
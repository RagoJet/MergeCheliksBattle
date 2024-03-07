using States;
using UnityEngine;
using UnityEngine.AI;

namespace Gameplay.Units.UnitStates
{
    public class BattleUnitState : IState
    {
        private Unit _unit;
        private NavMeshAgent _agent;
        private UnitAnimator _unitAnimator;
        private Health _targetHealth;

        private float _sqrRange;
        private bool _isAttacking = false;

        public BattleUnitState(Unit unit)
        {
            _unit = unit;
            _agent = unit.GetComponent<NavMeshAgent>();
            _sqrRange = _agent.stoppingDistance * _agent.stoppingDistance;
            _unitAnimator = unit.GetComponent<UnitAnimator>();
        }

        public void Tick()
        {
            float sqrDistance = (_unit.transform.position - _targetHealth.transform.position).sqrMagnitude;
            if (_isAttacking)
            {
                if (_targetHealth.IsAlive == false)
                {
                    _isAttacking = false;
                    _unitAnimator.SetFreeTrigger();
                    SetNewTarget();
                    return;
                }

                if (sqrDistance > _sqrRange)
                {
                    _isAttacking = false;
                    _unitAnimator.SetFreeTrigger();
                }
                else
                {
                    Vector3 direction = Vector3.Lerp(_unit.transform.forward,
                        _targetHealth.transform.position - _unit.transform.position, Time.deltaTime * 5);
                    _unit.transform.forward = direction;
                }
            }
            else // isAttacking == false
            {
                if (_targetHealth.IsAlive == false)
                {
                    SetNewTarget();
                    return;
                }


                if (sqrDistance <= _sqrRange)
                {
                    _isAttacking = true;
                    _unitAnimator.SetAttackTrigger();
                }
                else
                {
                    _unit.GoTo(_targetHealth.transform.position);
                }
            }
        }

        public void OnEnter()
        {
            SetNewTarget();
        }

        public void OnExit()
        {
            _unitAnimator.SetFreeTrigger();
        }

        private void SetNewTarget()
        {
            Unit targetUnit = _unit.GetClosestOpponent();

            if (targetUnit != null)
            {
                _targetHealth = targetUnit.GetComponent<Health>();
                _unit.SetTarget(_targetHealth);
                _unit.GoTo(_targetHealth.transform.position);
            }
            else
            {
                _targetHealth = null;
                _isAttacking = false;
                _unit.StopFight();
                _unit.SetTarget(null);
            }
        }
    }
}
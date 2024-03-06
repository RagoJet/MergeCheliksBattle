using States;
using UnityEngine;

namespace Gameplay.Units.UnitStates
{
    public class BattleUnitState : IState
    {
        private Unit _unit;
        private Health _targetHealth;
        private float _sqrRange;

        private bool _isAttacking = false;

        public BattleUnitState(Unit unit, float rangeAttack)
        {
            _unit = unit;
            _sqrRange = rangeAttack * rangeAttack;
        }

        public void Tick()
        {
            switch (_isAttacking)
            {
                case false:
                    if (_targetHealth.IsAlive == false)
                    {
                        SetNewTarget();
                        return;
                    }

                    if ((_unit.transform.position - _targetHealth.transform.position).sqrMagnitude <= _sqrRange)
                    {
                        _isAttacking = true;
                        _unit.GetComponent<UnitAnimator>().SetAttackTrigger();
                    }
                    else
                    {
                        _unit.GoTo(_targetHealth.transform.position);
                    }

                    break;
                case true:
                    if (_targetHealth.IsAlive == false)
                    {
                        _isAttacking = false;
                        _unit.GetComponent<UnitAnimator>().SetFreeTrigger();
                        SetNewTarget();
                        return;
                    }

                    if ((_unit.transform.position - _targetHealth.transform.position).sqrMagnitude > _sqrRange)
                    {
                        _isAttacking = false;
                        _unit.GetComponent<UnitAnimator>().SetFreeTrigger();
                    }
                    else
                    {
                        Vector3 direction = Vector3.Lerp(_unit.transform.forward,
                            _targetHealth.transform.position - _unit.transform.position, Time.deltaTime * 5);
                        _unit.transform.forward = direction;
                    }

                    break;
            }
        }

        public void OnEnter()
        {
            SetNewTarget();
        }

        public void OnExit()
        {
            _unit.GetComponent<UnitAnimator>().SetFreeTrigger();
        }

        private void SetNewTarget()
        {
            Unit targetUnit = _unit.GetClosestOpponent();

            if (targetUnit != null)
            {
                _targetHealth = targetUnit.GetComponent<Health>();
                _unit.SetTarget(_targetHealth);
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
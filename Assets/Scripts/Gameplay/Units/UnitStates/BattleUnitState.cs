using System;
using States;
using UnityEngine;

namespace Gameplay.Units.UnitStates
{
    public class BattleUnitState : IState
    {
        private Unit _unit;
        private Health _targetHealth;
        private float _rangeAttack;

        private bool _isAttacking = false;

        public BattleUnitState(Unit unit, float rangeAttack)
        {
            _unit = unit;
            _rangeAttack = rangeAttack;
        }

        public void Tick()
        {
            switch (_isAttacking)
            {
                case false:
                    if ((_unit.transform.position - _targetHealth.transform.position).magnitude <= _rangeAttack)
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
                    }
                    else
                    {
                        _unit.transform.LookAt(_targetHealth.transform, Vector3.up);
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
                _unit.StopFight();
                _unit.SetTarget(null);
            }
        }
    }
}
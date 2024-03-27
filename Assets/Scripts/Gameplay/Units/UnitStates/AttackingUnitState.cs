using States;
using UnityEngine;

namespace Gameplay.Units.UnitStates
{
    public class AttackingUnitState : IState
    {
        private readonly Unit _unit;

        public AttackingUnitState(Unit unit)
        {
            _unit = unit;
        }

        public void Tick()
        {
            Vector3 direction = Vector3.Lerp(_unit.transform.forward,
                _unit.TargetHealth.transform.position - _unit.transform.position, Time.deltaTime * 5);
            _unit.transform.forward = direction;
        }

        public void OnEnter()
        {
            _unit.GetComponent<UnitAnimator>().SetAttackTrigger();
        }

        public void OnExit()
        {
            _unit.GetComponent<UnitAnimator>().SetFreeTrigger();
        }
    }
}
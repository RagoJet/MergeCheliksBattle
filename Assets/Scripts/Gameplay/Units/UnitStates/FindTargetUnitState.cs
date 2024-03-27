using States;

namespace Gameplay.Units.UnitStates
{
    public class FindTargetUnitState : IState
    {
        private readonly Unit _unit;

        public FindTargetUnitState(Unit unit)
        {
            _unit = unit;
        }

        public void Tick()
        {
        }

        public void OnEnter()
        {
            Unit targetUnit = _unit.GetClosestOpponent();

            if (targetUnit != null)
            {
                _unit.SetTarget(targetUnit.GetComponent<Health>());
            }
            else
            {
                _unit.StopFight();
                _unit.SetTarget(null);
            }
        }

        public void OnExit()
        {
        }
    }
}
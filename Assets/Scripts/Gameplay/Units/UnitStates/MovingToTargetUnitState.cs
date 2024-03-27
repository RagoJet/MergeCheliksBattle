using States;

namespace Gameplay.Units.UnitStates
{
    public class MovingToTargetUnitState : IState
    {
        private readonly Unit _unit;

        public MovingToTargetUnitState(Unit unit)
        {
            _unit = unit;
        }

        public void Tick()
        {
            _unit.GoTo(_unit.TargetHealth.transform.position);
        }

        public void OnEnter()
        {
        }

        public void OnExit()
        {
        }
    }
}
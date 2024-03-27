using System;
using States;

namespace Gameplay.Units.Crowds.CrowdOfUnitsState
{
    public class BattleCrowdState : IState
    {
        private readonly Action _onEnter;
        private readonly Action _onTick;

        public BattleCrowdState(Action onEnter, Action onTick)
        {
            _onEnter = onEnter;
            _onTick = onTick;
        }


        public void Tick()
        {
            _onTick.Invoke();
        }

        public void OnEnter()
        {
            _onEnter.Invoke();
        }

        public void OnExit()
        {
        }
    }
}
using System;
using States;

namespace Gameplay.Units.Crowds.CrowdOfUnitsState
{
    public class BattleCrowdState : IState
    {
        private Action _onEnter;
        private Action _onTick;

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
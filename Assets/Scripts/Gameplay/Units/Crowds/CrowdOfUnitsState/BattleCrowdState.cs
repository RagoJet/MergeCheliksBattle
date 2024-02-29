using System;
using States;

namespace Gameplay.Units.Crowds.CrowdOfUnitsState
{
    public class BattleCrowdState : IState
    {
        private Action _onEnter;

        public BattleCrowdState(Action onEnter)
        {
            _onEnter = onEnter;
        }


        public void Tick()
        {
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
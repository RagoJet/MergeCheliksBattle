using System;
using IState = States.IState;

namespace Gameplay.Units.Crowds.CrowdOfUnitsState
{
    public class VagrancyCrowdState : IState
    {
        private readonly Action _onVagrancy;

        public VagrancyCrowdState(Action onVagrancy)
        {
            _onVagrancy = onVagrancy;
        }

        public void Tick()
        {
            _onVagrancy.Invoke();
        }

        public void OnEnter()
        {
        }

        public void OnExit()
        {
        }
    }
}
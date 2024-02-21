using Gameplay;
using Gameplay.BeforeTheBattle;

namespace States.CreaturesState
{
    public class GoToEmptyCellState : IState
    {
        private CreatureMaster _creatureMaster;

        public GoToEmptyCellState(CreatureMaster creatureMaster)
        {
            _creatureMaster = creatureMaster;
        }

        public void Tick()
        {
        }

        public void OnEnter()
        {
            _creatureMaster.creatureTarget.ReleaseCurrentCell();
            _creatureMaster.creatureTarget.SetNewCell(_creatureMaster.cellTarget);
            _creatureMaster.creatureProcessed = true;
        }

        public void OnExit()
        {
        }
    }
}
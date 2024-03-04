using States;

namespace Gameplay.Cells.MasterOfCreaturesStates
{
    public class BackToCellState : IState
    {
        private CreatureMaster _creatureMaster;

        public BackToCellState(CreatureMaster creatureMaster)
        {
            _creatureMaster = creatureMaster;
        }

        public void Tick()
        {
          
        }

        public void OnEnter()
        {
            _creatureMaster.creatureTarget.BackToCell();
            _creatureMaster.creatureProcessed = true;
        }

        public void OnExit()
        {
          
        }
    }
}
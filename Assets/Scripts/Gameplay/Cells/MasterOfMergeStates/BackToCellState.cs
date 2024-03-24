using States;

namespace Gameplay.Cells.MasterOfMergeStates
{
    public class BackToCellState : IState
    {
        private MergeMaster _mergeMaster;

        public BackToCellState(MergeMaster mergeMaster)
        {
            _mergeMaster = mergeMaster;
        }

        public void Tick()
        {
          
        }

        public void OnEnter()
        {
            _mergeMaster.mergeEntityTarget.BackToCell();
            _mergeMaster.creatureProcessed = true;
        }

        public void OnExit()
        {
          
        }
    }
}
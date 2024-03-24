using States;

namespace Gameplay.Cells.MasterOfMergeStates
{
    public class GoToEmptyCellState : IState
    {
        private MergeMaster _mergeMaster;

        public GoToEmptyCellState(MergeMaster mergeMaster)
        {
            _mergeMaster = mergeMaster;
        }

        public void Tick()
        {
        }

        public void OnEnter()
        {
            _mergeMaster.mergeEntityTarget.ReleaseCurrentCell();
            _mergeMaster.mergeEntityTarget.SetNewCell(_mergeMaster.cellTarget);
            _mergeMaster.creatureProcessed = true;
        }

        public void OnExit()
        {
        }
    }
}
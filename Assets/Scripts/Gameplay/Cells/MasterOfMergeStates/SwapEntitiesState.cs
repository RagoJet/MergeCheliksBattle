using Gameplay.MergeEntities;
using States;

namespace Gameplay.Cells.MasterOfMergeStates
{
    public class SwapEntitiesState : IState
    {
        private MergeMaster _mergeMaster;

        public SwapEntitiesState(MergeMaster mergeMaster)
        {
            _mergeMaster = mergeMaster;
        }

        public void Tick()
        {
        }

        public void OnEnter()
        {
            Cell tempCell = _mergeMaster.mergeEntityTarget.CurrentCell;
            MergeEntity tempMergeEntity = _mergeMaster.cellTarget.currentMergeEntity;
            _mergeMaster.mergeEntityTarget.SetNewCell(_mergeMaster.cellTarget);
            tempMergeEntity.SetNewCell(tempCell);
            _mergeMaster.creatureProcessed = true;
        }

        public void OnExit()
        {
        }
    }
}
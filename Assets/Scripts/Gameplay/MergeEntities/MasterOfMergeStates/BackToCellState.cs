using States;

namespace Gameplay.MergeEntities.MasterOfMergeStates
{
    public class BackToCellState : IState
    {
        private readonly MergeMaster _mergeMaster;

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
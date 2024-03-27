using States;

namespace Gameplay.MergeEntities.MasterOfMergeStates
{
    public class MergeEntitiesState : IState
    {
        private readonly MergeMaster _mergeMaster;

        public MergeEntitiesState(MergeMaster mergeMaster)
        {
            _mergeMaster = mergeMaster;
        }

        public void Tick()
        {
        }

        public void OnEnter()
        {
            _mergeMaster.mergeEntityTarget.SetTo(_mergeMaster.cellTarget.GetPosition, () => OnSetTo());

            void OnSetTo()
            {
                _mergeMaster.Merge(_mergeMaster.mergeEntityTarget,
                    _mergeMaster.cellTarget.currentMergeEntity, _mergeMaster.cellTarget);
                _mergeMaster.creatureProcessed = true;
            }
        }

        public void OnExit()
        {
        }
    }
}
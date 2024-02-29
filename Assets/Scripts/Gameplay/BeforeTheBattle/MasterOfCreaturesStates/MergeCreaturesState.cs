using States;

namespace Gameplay.BeforeTheBattle.MasterOfCreaturesStates
{
    public class MergeCreaturesState : IState
    {
        private CreatureMaster _creatureMaster;

        public MergeCreaturesState(CreatureMaster creatureMaster)
        {
            _creatureMaster = creatureMaster;
        }

        public void Tick()
        {
        }

        public void OnEnter()
        {
            _creatureMaster.creatureTarget.SetTo(_creatureMaster.cellTarget.GetPosition, () => OnSetTo());

            void OnSetTo()
            {
                _creatureMaster.MergeCreatures(_creatureMaster.creatureTarget,
                    _creatureMaster.cellTarget.currentCreature, _creatureMaster.cellTarget);
                _creatureMaster.creatureProcessed = true;
            }
        }

        public void OnExit()
        {
        }
    }
}
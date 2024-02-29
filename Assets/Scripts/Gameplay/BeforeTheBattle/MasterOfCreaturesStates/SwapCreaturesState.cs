using Gameplay.Cells;
using Gameplay.Units.Creatures;
using States;

namespace Gameplay.BeforeTheBattle.MasterOfCreaturesStates
{
    public class SwapCreaturesState : IState
    {
        private CreatureMaster _creatureMaster;

        public SwapCreaturesState(CreatureMaster creatureMaster)
        {
            _creatureMaster = creatureMaster;
        }

        public void Tick()
        {
        }

        public void OnEnter()
        {
            Cell tempCell = _creatureMaster.creatureTarget.CurrentCell;
            Creature tempCreature = _creatureMaster.cellTarget.currentCreature;
            _creatureMaster.creatureTarget.SetNewCell(_creatureMaster.cellTarget);
            tempCreature.SetNewCell(tempCell);
            _creatureMaster.creatureProcessed = true;
        }

        public void OnExit()
        {
        }
    }
}
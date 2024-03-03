using System.Collections.Generic;
using Gameplay.Cells.MasterOfCreaturesStates;
using Gameplay.Units;
using Gameplay.Units.Creatures;
using Services;
using Services.Factories;
using Services.SaveLoad;
using States;
using UnityEngine;

namespace Gameplay.Cells
{
    public class CreatureMaster : MonoBehaviour
    {
        private StateMachine _stateMachine = new StateMachine();

        public Creature creatureTarget;
        public Cell cellTarget;
        public bool draggingCreature;
        public bool creatureProcessed;

        private int _islandLayerMask;
        private int _cellLayerMask;

        private CellGrid _cellGrid;
        private List<Unit> _currentCreatures = new List<Unit>();

        public List<Unit> CurrentCreatures => _currentCreatures;

        private void Awake()
        {
            _islandLayerMask = 1 << LayerMask.NameToLayer("Island");
            _cellLayerMask = 1 << LayerMask.NameToLayer("Cell");
        }

        private void Start()
        {
            Camera mainCamera = Camera.main;

            NoCreatureState noCreatureState = new NoCreatureState(this, _cellLayerMask, mainCamera);
            HasCreatureState hasCreatureState =
                new HasCreatureState(this, _cellLayerMask, _islandLayerMask, mainCamera);

            BackToCellState backToCellState = new BackToCellState(this);

            GoToEmptyCellState goToEmptyCellState = new GoToEmptyCellState(this);

            SwapCreaturesState swapCreaturesState = new SwapCreaturesState(this);

            MergeCreaturesState mergeCreaturesState = new MergeCreaturesState(this);

            _stateMachine.AddTransition(noCreatureState, hasCreatureState, () => draggingCreature);

            _stateMachine.AddTransition(hasCreatureState, backToCellState,
                () => draggingCreature == false && CheckNoNewCell());

            _stateMachine.AddTransition(hasCreatureState, goToEmptyCellState,
                () => draggingCreature == false && CheckCellIsEmpty());

            _stateMachine.AddTransition(hasCreatureState, swapCreaturesState,
                () => draggingCreature == false && CheckAnotherLevelHit());

            _stateMachine.AddTransition(hasCreatureState, mergeCreaturesState,
                () => draggingCreature == false && CheckSameLevelHit());

            _stateMachine.AddAnyTransition(noCreatureState, () => creatureProcessed);
            _stateMachine.SetState(noCreatureState);
        }


        private void Update()
        {
            _stateMachine.Tick();
        }

        public void Construct(CellGrid cellGrid)
        {
            _cellGrid = cellGrid;
            DataProgress dataProgress = AllServices.Container.Get<ISaveLoadService>().DataProgress;
            foreach (var cellDTO in dataProgress.cellsDTO)
            {
                Creature creature = AllServices.Container.Get<IGameFactory>().CreateCreature(cellDTO.levelOfCreature,
                    _cellGrid.GetCellByIndex(cellDTO.indexOfCell));
                CurrentCreatures.Add(creature);
            }
        }

        public void MergeCreatures(Creature creature1, Creature creature2, Cell cell)
        {
            int newLevel = creature1.Level + 1;

            CurrentCreatures.Remove(creature1);
            CurrentCreatures.Remove(creature2);
            Destroy(creature1.gameObject);
            Destroy(creature2.gameObject);

            Creature newCreature = AllServices.Container.Get<IGameFactory>().CreateCreature(newLevel, cell);
            CurrentCreatures.Add(newCreature);
        }

        public void CreateFirstLevelCreature(Cell cell)
        {
            int level = 0;
            Creature newCreature = AllServices.Container.Get<IGameFactory>().CreateCreature(level, cell);
            CurrentCreatures.Add(newCreature);
        }

        private bool CheckSameLevelHit()
        {
            if (cellTarget.currentCreature == null)
            {
                return false;
            }

            return cellTarget.currentCreature.Level == creatureTarget.Level;
        }

        private bool CheckAnotherLevelHit()
        {
            if (cellTarget.currentCreature == null)
            {
                return false;
            }

            return cellTarget.currentCreature.Level != creatureTarget.Level;
        }

        private bool CheckCellIsEmpty()
        {
            return cellTarget.currentCreature == null;
        }

        private bool CheckNoNewCell()
        {
            return cellTarget == creatureTarget.CurrentCell || cellTarget == null;
        }
    }
}
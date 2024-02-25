using System;
using System.Collections.Generic;
using Gameplay.BeforeTheBattle.CreaturesState;
using Gameplay.Cells;
using Gameplay.Units.Creatures;
using Services;
using Services.SaveLoad;
using States;
using UnityEngine;

namespace Gameplay.BeforeTheBattle
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
        private CreaturesPool _creaturesPool;
        private List<Creature> _currentCreatures = new List<Creature>();

        public List<Creature> CurrentCreatures => _currentCreatures;

        private void Awake()
        {
            _creaturesPool = new CreaturesPool();
            _islandLayerMask = 1 << LayerMask.NameToLayer("Island");
            _cellLayerMask = 1 << LayerMask.NameToLayer("Cell");
            AllServices.Container.Get<EventBus>().OnDeathCreature += _creaturesPool.AddToPool;
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
            foreach (var cellDTO in dataProgress._cellsDTO)
            {
                Creature creature = _creaturesPool.GetAndSet(cellDTO.levelOfCreature,
                    _cellGrid.GetCellByIndex(cellDTO.indexOfCell));
                _currentCreatures.Add(creature);
            }
        }

        public void MergeCreatures(Creature creature1, Creature creature2, Cell cell)
        {
            int newLevel = creature1.Level + 1;

            _currentCreatures.Remove(creature1);
            _currentCreatures.Remove(creature2);
            _creaturesPool.AddToPool(creature1);
            _creaturesPool.AddToPool(creature2);

            Creature newCreature = _creaturesPool.GetAndSet(newLevel, cell);
            _currentCreatures.Add(newCreature);
        }

        public void CreateFirstLevelCreature(Cell cell)
        {
            int level = 0;
            Creature newCreature = _creaturesPool.GetAndSet(level, cell);
            _currentCreatures.Add(newCreature);
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
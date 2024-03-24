using System.Collections.Generic;
using Gameplay.Cells.MasterOfMergeStates;
using Gameplay.MergeEntities;
using Services;
using Services.Audio;
using Services.Factories;
using Services.SaveLoad;
using States;
using UnityEngine;
using UnityEngine.Serialization;

namespace Gameplay.Cells
{
    public class MergeMaster : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _mergeEffect;
        private StateMachine _stateMachine = new StateMachine();

        [FormerlySerializedAs("creatureTarget")] public MergeEntity mergeEntityTarget;
        public Cell cellTarget;
        public bool draggingCreature;
        public bool creatureProcessed;

        private int _islandLayerMask;
        private int _cellLayerMask;

        private CellGrid _cellGrid;
        private List<MergeEntity> _currentMergeEntities = new List<MergeEntity>();

        public List<MergeEntity> CurrentMergeEntities => _currentMergeEntities;

        private void Awake()
        {
            _islandLayerMask = 1 << LayerMask.NameToLayer("Island");
            _cellLayerMask = 1 << LayerMask.NameToLayer("Cell");
            AllServices.Container.Get<EventBus>().onOpenedUIWindow += TurnOff;
            AllServices.Container.Get<EventBus>().onClosedUIWindow += TurnOn;
        }

        private void Start()
        {
            Camera mainCamera = Camera.main;

            NoEntityState noEntityState = new NoEntityState(this, _cellLayerMask, mainCamera);
            HasEntityState hasEntityState =
                new HasEntityState(this, _cellLayerMask, _islandLayerMask, mainCamera);

            BackToCellState backToCellState = new BackToCellState(this);

            GoToEmptyCellState goToEmptyCellState = new GoToEmptyCellState(this);

            SwapEntitiesState swapEntitiesState = new SwapEntitiesState(this);

            MergeEntitiesState mergeEntitiesState = new MergeEntitiesState(this);

            _stateMachine.AddTransition(noEntityState, hasEntityState, () => draggingCreature);

            _stateMachine.AddTransition(hasEntityState, backToCellState,
                () => draggingCreature == false && CheckNoNewCell());

            _stateMachine.AddTransition(hasEntityState, goToEmptyCellState,
                () => draggingCreature == false && CheckCellIsEmpty());

            _stateMachine.AddTransition(hasEntityState, swapEntitiesState,
                () => draggingCreature == false && CheckAnotherLevelHit());

            _stateMachine.AddTransition(hasEntityState, mergeEntitiesState,
                () => draggingCreature == false && CheckSameLevelHit());

            _stateMachine.AddAnyTransition(noEntityState, () => creatureProcessed);
            _stateMachine.SetState(noEntityState);
        }


        private void Update()
        {
            _stateMachine.Tick();
        }

        public void Construct(CellGrid cellGrid)
        {
            _cellGrid = cellGrid;
            SavedData savedData = AllServices.Container.Get<ISaveLoadService>().SavedData;
            foreach (var cellDTO in savedData.cellsDTO)
            {
                MergeEntity mergeEntity = AllServices.Container.Get<IGameFactory>().CreateMergeEntity(cellDTO.isRange,
                    cellDTO.levelOfCreature,
                    _cellGrid.GetCellByIndex(cellDTO.indexOfCell));
                CurrentMergeEntities.Add(mergeEntity);
            }
        }

        public void Merge(MergeEntity firstEntity, MergeEntity secondEntity, Cell cell)
        {
            int newLevel = firstEntity.Level + 1;
            bool isRange = firstEntity.IsRange;

            CurrentMergeEntities.Remove(firstEntity);
            CurrentMergeEntities.Remove(secondEntity);
            Destroy(firstEntity.gameObject);
            Destroy(secondEntity.gameObject);

            MergeEntity newMergeEntity = AllServices.Container.Get<IGameFactory>().CreateMergeEntity(isRange, newLevel, cell);
            CurrentMergeEntities.Add(newMergeEntity);
            AllServices.Container.Get<IAudioService>().MergeSound();
            _mergeEffect.transform.position = newMergeEntity.transform.position;
            _mergeEffect.Play();
        }

        public void CreateMeleeFirstLevel(Cell cell)
        {
            int level = 0;
            MergeEntity newMergeEntity = AllServices.Container.Get<IGameFactory>().CreateMergeEntity(false, level, cell);
            CurrentMergeEntities.Add(newMergeEntity);
        }

        public void CreateRangeFirstLevel(Cell cell)
        {
            int level = 0;
            MergeEntity newMergeEntity = AllServices.Container.Get<IGameFactory>().CreateMergeEntity(true, level, cell);
            CurrentMergeEntities.Add(newMergeEntity);
        }

        private bool CheckSameLevelHit()
        {
            if (cellTarget.currentMergeEntity == null)
            {
                return false;
            }

            if (cellTarget.currentMergeEntity.Level == 11) return false;

            return cellTarget.currentMergeEntity.Level == mergeEntityTarget.Level &&
                   cellTarget.currentMergeEntity.IsRange == mergeEntityTarget.IsRange;
        }

        private bool CheckAnotherLevelHit()
        {
            if (cellTarget.currentMergeEntity == null)
            {
                return false;
            }

            if (cellTarget.currentMergeEntity.Level == 11) return true;

            return cellTarget.currentMergeEntity.Level != mergeEntityTarget.Level ||
                   cellTarget.currentMergeEntity.IsRange != mergeEntityTarget.IsRange;
        }

        private bool CheckCellIsEmpty()
        {
            return cellTarget.currentMergeEntity == null;
        }

        private bool CheckNoNewCell()
        {
            return cellTarget == mergeEntityTarget.CurrentCell || cellTarget == null;
        }

        private void TurnOff()
        {
            gameObject.SetActive(false);
        }

        private void TurnOn()
        {
            gameObject.SetActive(true);
        }

        private void OnDestroy()
        {
            AllServices.Container.Get<EventBus>().onOpenedUIWindow -= TurnOff;
            AllServices.Container.Get<EventBus>().onClosedUIWindow -= TurnOn;
        }
    }
}
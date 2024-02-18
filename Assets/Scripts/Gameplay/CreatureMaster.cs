using System.Collections.Generic;
using Gameplay.Creatures;
using Services;
using Services.SaveLoad;
using UnityEngine;

namespace Gameplay
{
    public class CreatureMaster : MonoBehaviour
    {
        private CellGrid _cellGrid;

        private Creature _targetCreature;
        private bool _hasCreature;

        private int _islandLayerMask;
        private int _creatureLayerMask;
        private int _cellLayerMask;

        private CreaturesPool _creaturesPool;

        private List<Creature> _currentCreatures = new List<Creature>();

        private void Awake()
        {
            _islandLayerMask = 1 << LayerMask.NameToLayer("Island");
            _creatureLayerMask = 1 << LayerMask.NameToLayer("Creature");
            _cellLayerMask = 1 << LayerMask.NameToLayer("Cell");

            _creaturesPool = new CreaturesPool();
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

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, Mathf.Infinity, _creatureLayerMask))
                {
                    if (hit.collider.TryGetComponent(out Creature creature))
                    {
                        _hasCreature = true;
                        _targetCreature = creature;
                        _targetCreature.GetUp();
                    }
                }

                return;
            }

            if (Input.GetMouseButton(0) && _hasCreature)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, Mathf.Infinity, _islandLayerMask))
                {
                    if (hit.collider)
                    {
                        Vector3 newPosition =
                            new Vector3(hit.point.x, _targetCreature.transform.position.y, hit.point.z);
                        _targetCreature.transform.position = newPosition;
                    }
                }
            }

            if (Input.GetMouseButtonUp(0) && _hasCreature)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, Mathf.Infinity, _cellLayerMask))
                {
                    if (hit.collider.TryGetComponent(out Cell cell))
                    {
                        if (cell.currentCreature == null)
                        {
                            _targetCreature.ReleaseCell();
                            _targetCreature.SetNewCell(cell);
                            _targetCreature.ReturnToCell();
                        }
                        else if (cell == _targetCreature.CurrentCell)
                        {
                            _targetCreature.ReturnToCell();
                        }
                        else if (cell.currentCreature.Level == _targetCreature.Level)
                        {
                            Creature movingCreature = _targetCreature;
                            _targetCreature.SetTo(cell.GetPosition,
                                () => { MergeCreatures(movingCreature, cell.currentCreature, cell); });
                        }
                        else
                        {
                            Cell tempCell = _targetCreature.CurrentCell;
                            Creature tempCreature = cell.currentCreature;
                            _targetCreature.SetNewCell(cell);
                            tempCreature.SetNewCell(tempCell);
                        }
                    }
                }
                else
                {
                    _targetCreature.ReturnToCell();
                }

                _hasCreature = false;
                _targetCreature = null;
            }
        }

        private void MergeCreatures(Creature creature1, Creature creature2, Cell cell)
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

        public void CollidersCreatureOn()
        {
            foreach (var creature in _currentCreatures)
            {
                creature.ColliderOn();
            }
        }

        public void CollidersCreatureOff()
        {
            foreach (var creature in _currentCreatures)
            {
                creature.ColliderOff();
            }
        }
    }
}
using Gameplay.Cells;
using States;
using UnityEngine;

namespace Gameplay.BeforeTheBattle.CreaturesState
{
    public class NoCreatureState : IState
    {
        private int _layerMask;
        private CreatureMaster _creatureMaster;
        private Camera _mainCamera;

        public NoCreatureState(CreatureMaster creatureMaster, int layerMask, Camera mainCamera)
        {
            _creatureMaster = creatureMaster;
            _layerMask = layerMask;
            _mainCamera = mainCamera;
        }

        public void Tick()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, Mathf.Infinity, _layerMask))
                {
                    if (hit.collider.TryGetComponent(out Cell cell))
                    {
                        if (cell.currentCreature != null)
                        {
                            _creatureMaster.creatureTarget = cell.currentCreature;
                            _creatureMaster.draggingCreature = true;
                        }
                    }
                }
            }
        }

        public void OnEnter()
        {
            _creatureMaster.creatureProcessed = false;
            _creatureMaster.creatureTarget = null;
            _creatureMaster.cellTarget = null;
        }

        public void OnExit()
        {
        }
    }
}
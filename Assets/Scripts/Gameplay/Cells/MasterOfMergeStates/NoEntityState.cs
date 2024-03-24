using States;
using UnityEngine;

namespace Gameplay.Cells.MasterOfMergeStates
{
    public class NoEntityState : IState
    {
        private int _layerMask;
        private MergeMaster _mergeMaster;
        private Camera _mainCamera;

        public NoEntityState(MergeMaster mergeMaster, int layerMask, Camera mainCamera)
        {
            _mergeMaster = mergeMaster;
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
                        if (cell.currentMergeEntity != null)
                        {
                            _mergeMaster.mergeEntityTarget = cell.currentMergeEntity;
                            _mergeMaster.draggingCreature = true;
                        }
                    }
                }
            }
        }

        public void OnEnter()
        {
            _mergeMaster.creatureProcessed = false;
            _mergeMaster.mergeEntityTarget = null;
            _mergeMaster.cellTarget = null;
        }

        public void OnExit()
        { }
    }
}
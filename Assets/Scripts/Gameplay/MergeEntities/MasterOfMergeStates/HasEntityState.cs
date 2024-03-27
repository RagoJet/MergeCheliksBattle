using Gameplay.Cells;
using Services;
using Services.Audio;
using States;
using UnityEngine;

namespace Gameplay.MergeEntities.MasterOfMergeStates
{
    public class HasEntityState : IState
    {
        private readonly MergeMaster _mergeMaster;
        private readonly int _islandLayerMask;
        private readonly int _cellLayerMask;

        private readonly Camera _mainCamera;

        public HasEntityState(MergeMaster mergeMaster, int cellLayerMask, int islandLayerMask,
            Camera mainCamera)
        {
            _mergeMaster = mergeMaster;
            _cellLayerMask = cellLayerMask;
            _islandLayerMask = islandLayerMask;
            _mainCamera = mainCamera;
        }

        public void Tick()
        {
            Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, _islandLayerMask))
            {
                if (hit.collider)
                {
                    Transform creatureTransform = _mergeMaster.mergeEntityTarget.transform;
                    Vector3 newPosition = new Vector3(hit.point.x, creatureTransform.position.y, hit.point.z);
                    creatureTransform.position = newPosition;
                }
            }

            if (Input.GetMouseButtonUp(0))
            {
                if (Physics.Raycast(ray, out hit, Mathf.Infinity, _cellLayerMask))
                {
                    if (hit.collider.TryGetComponent(out Cell cell))
                    {
                        _mergeMaster.cellTarget = cell;
                    }
                }

                _mergeMaster.draggingCreature = false;
            }
        }

        public void OnEnter()
        {
            _mergeMaster.mergeEntityTarget.GetUp();
            AllServices.Container.Get<IAudioService>().PickUpMergeEntitySound();
        }

        public void OnExit()
        {
            AllServices.Container.Get<IAudioService>().DownMergeEntitySound();
        }
    }
}
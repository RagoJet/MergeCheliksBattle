using Services;
using Services.Audio;
using States;
using UnityEngine;

namespace Gameplay.Cells.MasterOfMergeStates
{
    public class HasEntityState : IState
    {
        public MergeMaster MergeMaster;
        private int _islandlayerMask;
        private int _cellLayerMask;

        private Camera _mainCamera;

        public HasEntityState(MergeMaster mergeMaster, int cellLayerMask, int islandlayerMask,
            Camera mainCamera)
        {
            MergeMaster = mergeMaster;
            _cellLayerMask = cellLayerMask;
            _islandlayerMask = islandlayerMask;
            _mainCamera = mainCamera;
        }

        public void Tick()
        {
            Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, _islandlayerMask))
            {
                if (hit.collider)
                {
                    Transform creatureTransform = MergeMaster.mergeEntityTarget.transform;
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
                        MergeMaster.cellTarget = cell;
                    }
                }

                MergeMaster.draggingCreature = false;
            }
        }

        public void OnEnter()
        {
            MergeMaster.mergeEntityTarget.GetUp();
            AllServices.Container.Get<IAudioService>().PickUpMergeEntitySound();
        }

        public void OnExit()
        {
            AllServices.Container.Get<IAudioService>().DownMergeEntitySound();
        }
    }
}
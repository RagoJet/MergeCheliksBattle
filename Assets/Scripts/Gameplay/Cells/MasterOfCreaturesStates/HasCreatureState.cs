using Services;
using Services.Audio;
using States;
using UnityEngine;

namespace Gameplay.Cells.MasterOfCreaturesStates
{
    public class HasCreatureState : IState
    {
        public CreatureMaster _creatureMaster;
        private int _islandlayerMask;
        private int _cellLayerMask;

        private Camera _mainCamera;

        public HasCreatureState(CreatureMaster creatureMaster, int cellLayerMask, int islandlayerMask,
            Camera mainCamera)
        {
            _creatureMaster = creatureMaster;
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
                    Transform creatureTransform = _creatureMaster.creatureTarget.transform;
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
                        _creatureMaster.cellTarget = cell;
                    }
                }

                _creatureMaster.draggingCreature = false;
            }
        }

        public void OnEnter()
        {
            _creatureMaster.creatureTarget.GetUp();
            AllServices.Container.Get<IAudioService>().PickUpCreatureSound();
        }

        public void OnExit()
        {
            AllServices.Container.Get<IAudioService>().DownCreatureSound();
        }
    }
}
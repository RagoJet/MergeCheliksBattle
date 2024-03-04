using Services;
using UnityEngine;

namespace Gameplay.Units.Crowds
{
    public class EnemyCrowdPointer : MonoBehaviour
    {
        private Camera _camera;
        private Transform _playerCrowd;
        private Transform _pointerIconTransform;

        private bool _isReady;

        private void Awake()
        {
            _camera = Camera.main;
            AllServices.Container.Get<EventBus>().onDeathCreatureCrowd += SwitchOff;
        }

        public void Construct(Transform playerCrowdTransform, Transform pointerImage)
        {
            _playerCrowd = playerCrowdTransform;
            _pointerIconTransform = pointerImage;
            _isReady = true;
        }

        private void Update()
        {
            if (_isReady == false)
            {
                return;
            }

            Vector3 fromPlayerToEnemy = transform.position - _playerCrowd.transform.position;
            Ray ray = new Ray(_playerCrowd.transform.position, fromPlayerToEnemy);

            // Ordering: [0] = Left, [1] = Right, [2] = Down, [3] = Up
            Plane[] planes = GeometryUtility.CalculateFrustumPlanes(_camera);
            int planeIndex = 0;
            float minDistance = Mathf.Infinity;

            for (int i = 0; i < 4; i++)
            {
                if (planes[i].Raycast(ray, out float distance))
                {
                    if (distance < minDistance)
                    {
                        minDistance = distance;
                        planeIndex = i;
                    }
                }
            }

            float sqrDistance = minDistance * minDistance;
            if (sqrDistance >= fromPlayerToEnemy.sqrMagnitude)
            {
                _pointerIconTransform.gameObject.SetActive(false);
            }
            else
            {
                _pointerIconTransform.gameObject.SetActive(true);
                Vector3 worldPosition = ray.GetPoint(minDistance);
                _pointerIconTransform.position = _camera.WorldToScreenPoint(worldPosition);
                _pointerIconTransform.rotation = GetIconRotation(planeIndex);
            }
        }

        Quaternion GetIconRotation(int planeIndex)
        {
            return planeIndex switch
            {
                0 => Quaternion.Euler(0, 0, 90),
                1 => Quaternion.Euler(0, 0, -90),
                2 => Quaternion.Euler(0, 0, 180),
                3 => Quaternion.Euler(0, 0, 0),
                _ => Quaternion.identity
            };
        }

        private void SwitchOff()
        {
            enabled = false;
            Destroy(_pointerIconTransform.gameObject);
        }

        private void OnDisable()
        {
            AllServices.Container.Get<EventBus>().onDeathCreatureCrowd -= SwitchOff;
        }

        private void OnDestroy()
        {
            if (_pointerIconTransform != null)
            {
                Destroy(_pointerIconTransform.gameObject);
            }
        }
    }
}
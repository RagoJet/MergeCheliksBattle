using System.Collections;
using Gameplay.Units.Crowds.CrowdOfUnitsState;
using Services;
using Services.Audio;
using Services.JoySticks;
using UnityEngine;

namespace Gameplay.Units.Crowds
{
    [RequireComponent(typeof(Rigidbody))]
    public class CrowdOfMerged : CrowdOfUnits
    {
        [SerializeField] private int _speedCameraRotation;
        [SerializeField] private int _XAngle;

        [SerializeField] private int _ZOffset;
        [SerializeField] private float _cameraSpeed;

        private IJoyStick _joyStick;
        Camera _mainCamera;

        private void Start()
        {
            _joyStick = AllServices.Container.Get<IJoyStick>();
            _mainCamera = Camera.main;

            VagrancyCrowdState vagrancyCrowdState = new VagrancyCrowdState(HandleCrowd);
            BattleCrowdState
                battleCrowdState =
                    new BattleCrowdState(() => fightMode = true, ChangePositionToUnits);

            _stateMachine.AddTransition(vagrancyCrowdState, battleCrowdState, () => fightMode);
            _stateMachine.AddTransition(battleCrowdState, vagrancyCrowdState, () => fightMode == false);
            _stateMachine.SetState(vagrancyCrowdState);
            StartCoroutine(ChangeCameraRotation());
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out CrowdOfEnemies crowdOfEnemies))
            {
                if (fightMode == false)
                {
                    StartFight(crowdOfEnemies);
                    crowdOfEnemies.StartFight(this);
                }
            }
        }

        private void Update()
        {
            _stateMachine.Tick();
        }

        private void HandleCrowd()
        {
            base.FormatUnits();
            transform.forward = Vector3.Lerp(transform.forward, _joyStick.GetDirection(), Time.deltaTime * 5);
            transform.Translate(_joyStick.GetDirection() * (Time.deltaTime * 8), Space.World);
        }

        private void LateUpdate()
        {
            Vector3 destination = new Vector3(transform.position.x, _mainCamera.transform.position.y,
                transform.position.z - _ZOffset);
            _mainCamera.transform.position =
                Vector3.Lerp(_mainCamera.transform.position, destination, Time.deltaTime * _cameraSpeed);
        }

        private IEnumerator ChangeCameraRotation()
        {
            float x = _mainCamera.transform.rotation.eulerAngles.x;
            if (x > _XAngle)
            {
                while (x > _XAngle)
                {
                    x -= Time.deltaTime * _speedCameraRotation;
                    yield return null;
                    _mainCamera.transform.rotation = Quaternion.Euler(x, 0, 0);
                }
            }
            else
            {
                while (x < _XAngle)
                {
                    x += Time.deltaTime * _speedCameraRotation;
                    yield return null;
                    _mainCamera.transform.rotation = Quaternion.Euler(x, 0, 0);
                }
            }


            _mainCamera.transform.rotation = Quaternion.Euler(_XAngle, 0, 0);
        }

        protected override void Die()
        {
            AllServices.Container.Get<EventBus>().OnDeathCreatureCrowd();
            AllServices.Container.Get<IAudioService>().LoseSound();
            base.Die();
        }
    }
}
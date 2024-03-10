using System.Collections;
using Gameplay.Units.Crowds.CrowdOfUnitsState;
using Services;
using Services.Audio;
using Services.JoySticks;
using UnityEngine;

namespace Gameplay.Units.Crowds
{
    [RequireComponent(typeof(Rigidbody))]
    public class CrowdOfCreatures : CrowdOfUnits
    {
        [SerializeField] private int _speedCameraRotation;
        [SerializeField] private int _XAngle;

        [SerializeField] private int _ZOffset;
        [SerializeField] private float _cameraSpeed;

        private IJoyStick _joyStick;
        Camera _mainCamera;
        private float _YHeightOfCamera;

        private void Start()
        {
            _joyStick = AllServices.Container.Get<IJoyStick>();
            _mainCamera = Camera.main;

            _YHeightOfCamera = _mainCamera.transform.position.y + 3;

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
                    AllServices.Container.Get<IAudioService>().PlayFightSound();
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
            transform.Translate(_joyStick.GetDirection() * Time.deltaTime * 8f);
        }

        private void LateUpdate()
        {
            Vector3 destination = new Vector3(transform.position.x, _YHeightOfCamera, transform.position.z - _ZOffset);
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
            AllServices.Container.Get<IAudioService>().PlayLoseSound();
            base.Die();
        }
    }
}
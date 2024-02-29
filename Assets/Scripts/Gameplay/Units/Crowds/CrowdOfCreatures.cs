using Gameplay.Units.Crowds.CrowdOfUnitsState;
using Services;
using Services.JoySticks;
using UnityEngine;

namespace Gameplay.Units.Crowds
{
    [RequireComponent(typeof(Rigidbody))]
    public class CrowdOfCreatures : CrowdOfUnits
    {
        private IJoyStick _joyStick;
        Camera _mainCamera;


        private void Start()
        {
            _joyStick = AllServices.Container.Get<IJoyStick>();
            _mainCamera = Camera.main;

            VagrancyCrowdState vagrancyCrowdState = new VagrancyCrowdState(HandleCrowd);
            BattleCrowdState battleCrowdState = new BattleCrowdState(() => fightMode = true); // replace actionEnter

            _stateMachine.AddTransition(vagrancyCrowdState, battleCrowdState, () => fightMode);
            _stateMachine.AddTransition(battleCrowdState, vagrancyCrowdState, () => fightMode == false);
            _stateMachine.SetState(vagrancyCrowdState);
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
            transform.Translate(_joyStick.GetDirection() * Time.deltaTime * 5);
        }

        private void LateUpdate()
        {
            Vector3 pos = Vector3.Lerp(_mainCamera.transform.position, transform.position, Time.deltaTime);
            pos.y = _mainCamera.transform.position.y;
            _mainCamera.transform.position = pos;
        }
    }
}
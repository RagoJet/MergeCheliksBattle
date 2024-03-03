using System;
using UnityEngine;

namespace Services.JoySticks
{
    public class MyJoyStick : MonoBehaviour, IJoyStick
    {
        [SerializeField] private Joystick _joystick;

        private Camera _mainCamera;

        private void OnEnable()
        {
            AllServices.Container.Get<EventBus>().onAllDeadEnemies += SwitchOff;
            AllServices.Container.Get<EventBus>().onDeathCreatureCrowd += SwitchOff;
        }

        private void OnDisable()
        {
            AllServices.Container.Get<EventBus>().onAllDeadEnemies -= SwitchOff;
            AllServices.Container.Get<EventBus>().onDeathCreatureCrowd -= SwitchOff;
        }


        public Vector3 GetDirection()
        {
            Vector3 joyStickeDirection = Vector3.right * _joystick.Horizontal + Vector3.forward * _joystick.Vertical;
            Vector3 direction = _mainCamera.transform.TransformDirection(joyStickeDirection);
            direction.y = 0;
            return direction.normalized;
        }


        public void SwitchOff()
        {
            gameObject.SetActive(false);
        }

        public void SwitchOn()
        {
            gameObject.SetActive(true);
            _mainCamera = Camera.main;
        }
    }
}
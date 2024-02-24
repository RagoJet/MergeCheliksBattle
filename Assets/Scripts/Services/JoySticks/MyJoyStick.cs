using UnityEngine;

namespace Services.JoySticks
{
    public class MyJoyStick : MonoBehaviour, IJoyStick
    {
        [SerializeField] private Joystick _joystick;

        private Camera _mainCamera;

        private void Awake()
        {
            _mainCamera = Camera.main;
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
        }
    }
}
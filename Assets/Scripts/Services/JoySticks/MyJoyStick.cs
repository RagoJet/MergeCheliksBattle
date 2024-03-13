using UnityEngine;
using UnityEngine.UI;

namespace Services.JoySticks
{
    public class MyJoyStick : MonoBehaviour, IJoyStick
    {
        [SerializeField] private Joystick _joystick;
        [SerializeField] private Image _topLeft;
        [SerializeField] private Image _topRight;
        [SerializeField] private Image _botRight;
        [SerializeField] private Image _botLeft;

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
            Vector3 joyStickeDirection = new Vector3(_joystick.Direction.x, 0, _joystick.Direction.y);
            Vector3 direction = _mainCamera.transform.TransformDirection(joyStickeDirection);
            direction.y = 0;
            LightCorner();
            return direction.normalized;
        }

        private void LightCorner()
        {
            _topLeft.gameObject.SetActive(false);
            _topRight.gameObject.SetActive(false);
            _botRight.gameObject.SetActive(false);
            _botLeft.gameObject.SetActive(false);

            Vector2 direction = _joystick.Direction;
            if (direction.x > 0 && direction.y > 0)
            {
                _topRight.gameObject.SetActive(true);
                return;
            }

            if (direction.x > 0 && direction.y < 0)
            {
                _botRight.gameObject.SetActive(true);
                return;
            }

            if (direction.x < 0 && direction.y > 0)
            {
                _topLeft.gameObject.SetActive(true);
                return;
            }

            if (direction.x < 0 && direction.y < 0)
            {
                _botLeft.gameObject.SetActive(true);
                return;
            }

            if (direction.x == 0 && direction.y > 0)
            {
                _topLeft.gameObject.SetActive(true);
                _topRight.gameObject.SetActive(true);
                return;
            }

            if (direction.x == 0 && direction.y < 0)
            {
                _botRight.gameObject.SetActive(true);
                _botLeft.gameObject.SetActive(true);
                return;
            }

            if (direction.x > 0 && direction.y == 0)
            {
                _botRight.gameObject.SetActive(true);
                _topRight.gameObject.SetActive(true);
                return;
            }

            if (direction.x < 0 && direction.y == 0)
            {
                _topLeft.gameObject.SetActive(true);
                _botLeft.gameObject.SetActive(true);
                return;
            }
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
using Gameplay.Units.Creatures;
using Services;
using Services.JoySticks;
using UnityEngine;

namespace Gameplay.Units.Crowds
{
    public class CrowdOfCreatures : CrowdOfUnits<Creature>
    {
        private IJoyStick _joyStick;

        Camera _mainCamera;

        private void Start()
        {
            _joyStick = AllServices.Container.Get<IJoyStick>();
            _mainCamera = Camera.main;
        }

        private void Update()
        {
            base.OnUpdate();
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
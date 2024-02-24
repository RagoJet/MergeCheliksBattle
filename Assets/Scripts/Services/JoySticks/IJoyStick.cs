using UnityEngine;

namespace Services.JoySticks
{
    public interface IJoyStick : IService
    {
        public Vector3 GetDirection();

        public void SwitchOff();
        public void SwitchOn();
    }
}
using System;
using UnityEngine;

namespace Gameplay
{
    public class Health : MonoBehaviour
    {
        private int _value;
        private int _maxValue;
        public event Action OnDie;
        private bool _isAlive = true;

        public bool IsAlive => _isAlive;

        public void Refresh(int value)
        {
            _maxValue = value;
            _value = _maxValue;
            _isAlive = true;
        }

        public void TakeDamage(int value)
        {
            if (value > 0)
            {
                _value -= value;
                if (_value <= 0)
                {
                    _isAlive = false;
                    _value = 0;
                    OnDie.Invoke();
                }
            }
        }

        public void Heal(int value)
        {
            if (value > 0)
            {
                _value += value;
                if (_value > _maxValue)
                {
                    _value = _maxValue;
                }
            }
        }
    }
}
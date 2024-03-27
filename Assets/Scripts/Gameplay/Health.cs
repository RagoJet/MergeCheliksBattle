using System;
using UnityEngine;

namespace Gameplay
{
    public class Health : MonoBehaviour
    {
        private int _value;
        private int _maxValue;
        public event Action onDie;
        private bool _isAlive = true;


        public event Action<int> onTakeDamage;
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
                if (value > _value)
                {
                    value = _value;
                }

                _value -= value;
                onTakeDamage.Invoke(value);
                if (_value <= 0)
                {
                    _isAlive = false;
                    _value = 0;
                    onDie.Invoke();
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
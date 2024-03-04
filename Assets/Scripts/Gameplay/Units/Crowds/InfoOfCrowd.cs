using System;
using TMPro;
using UnityEngine;

namespace Gameplay.Units.Crowds
{
    public class InfoOfCrowd : MonoBehaviour
    {
        [SerializeField] private Transform canvasInfo;
        [SerializeField] private TextMeshProUGUI _healthText;
        [SerializeField] private TextMeshProUGUI _damageText;
        private int _health;
        private int _damage;
        private Quaternion _rotationForCanvas;

        private void Awake()
        {
            _rotationForCanvas = Camera.main.transform.rotation;
        }

        public void Init()
        {
            CrowdOfUnits crowdOfUnits = GetComponent<CrowdOfUnits>();
            _health = crowdOfUnits.GetAllHealthOfUnits();
            _damage = crowdOfUnits.GetAllDamageOfUnits();
            UpdateHealthText();
            UpdateDamageText();
        }

        private void LateUpdate()
        {
            canvasInfo.rotation = _rotationForCanvas;
        }

        public void RemoveDamage(int value)
        {
            _damage -= value;
            UpdateDamageText();
        }

        public void RemoveHealth(int value)
        {
            _health -= value;
            UpdateHealthText();
        }

        private void UpdateHealthText()
        {
            _healthText.text = _health.ToString();
        }

        private void UpdateDamageText()
        {
            _damageText.text = _damage.ToString();
        }
    }
}
using System.Collections.Generic;
using AssetKits.ParticleImage;
using DG.Tweening;
using Services;
using Services.SaveLoad;
using TMPro;
using UnityEngine;

namespace Gameplay.UI
{
    public class InfoPanel : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _moneyText;
        [SerializeField] private TextMeshProUGUI _levelText;


        [SerializeField] private ParticleImage _moneyParticlePrefab;

        private List<ParticleImage> _moneyParticles = new List<ParticleImage>();

        private Wallet _wallet;
        private Tween _tween;
        private int _money;
        private int _addValue = 10;

        public void Construct(Wallet wallet)
        {
            _wallet = wallet;
            UpdateLevelText();
            InstantUpdateMoneyText();

            AllServices.Container.Get<EventBus>().onBuy += InstantUpdateMoneyText;
            AllServices.Container.Get<EventBus>().onGetGoldFrom += UpdateUIMoney;


            _moneyParticles.Add(CreateMoneyParticle());
        }

        private ParticleImage CreateMoneyParticle()
        {
            ParticleImage moneyParticle = Instantiate(_moneyParticlePrefab, transform);
            moneyParticle.attractorTarget = _moneyText.transform;
            moneyParticle.onAnyParticleFinished.AddListener(AddMoneyToText);

            return moneyParticle;
        }

        private void UpdateLevelText()
        {
            _levelText.text = $"Level : {AllServices.Container.Get<ISaveLoadService>().DataProgress.levelOfGame}";
        }

        private void InstantUpdateMoneyText()
        {
            _money = _wallet.Money;
            _moneyText.text = $"Money: {_money}";
        }


        private void UpdateUIMoney(Transform emitTransform, int value)
        {
            foreach (var particleImage in _moneyParticles)
            {
                if (particleImage.isPlaying == false)
                {
                    particleImage.emitterConstraintTransform = emitTransform;
                    particleImage.rateOverLifetime = value / _addValue;
                    particleImage.Play();
                    return;
                }
            }

            ParticleImage newParticleImage = CreateMoneyParticle();
            newParticleImage.emitterConstraintTransform = emitTransform;
            newParticleImage.rateOverLifetime = value / _addValue;
            newParticleImage.Play();
            _moneyParticles.Add(newParticleImage);
        }

        private void AddMoneyToText()
        {
            _money += _addValue;
            _moneyText.text = $"Money: {_money}";
        }

        private void OnDestroy()
        {
            AllServices.Container.Get<EventBus>().onBuy -= InstantUpdateMoneyText;
            AllServices.Container.Get<EventBus>().onGetGoldFrom -= UpdateUIMoney;
        }
    }
}
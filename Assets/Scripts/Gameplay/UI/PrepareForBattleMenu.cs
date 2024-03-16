using System;
using Gameplay.Cells;
using Gameplay.Units.Crowds;
using Services;
using Services.Ads;
using Services.Audio;
using Services.JoySticks;
using Services.SaveLoad;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Gameplay.UI
{
    public class PrepareForBattleMenu : MonoBehaviour
    {
        private CellGrid _grid;
        private CreatureMaster _creatureMaster;
        private SpawnerCrowds _spawnerCrowds;
        private Wallet _wallet;

        [SerializeField] private int _priceCreature = 200;

        [SerializeField] private Button _adsButton;
        [SerializeField] private Button _startBattleButton;
        [SerializeField] private Button _buyMeleeButton;
        [SerializeField] private Button _buyRangeButton;
        [SerializeField] private TextMeshProUGUI _priceRangeText;
        [SerializeField] private TextMeshProUGUI _priceMeleeText;

        private void Awake()
        {
            _startBattleButton.onClick.AddListener(StartBattle);
            _buyMeleeButton.onClick.AddListener(TryBuyMeleeCreature);
            _buyRangeButton.onClick.AddListener(TryBuyRangeCreature);
            _priceMeleeText.text = _priceCreature.ToString();
            _priceRangeText.text = _priceCreature.ToString();

            if (AllServices.Container.Get<ISaveLoadService>()
                    .SavedData.dateTimeExpirationSub.ToDateTime().CompareTo(DateTime.Now) > 0)
            {
                Destroy(_adsButton.gameObject);
            } else
            {
                AllServices.Container.Get<IAdsService>().HideBanner();
            }
        }

        public void Construct(CellGrid grid, CreatureMaster creatureMaster, SpawnerCrowds spawnerCrowds, Wallet wallet)
        {
            _grid = grid;
            _creatureMaster = creatureMaster;
            _spawnerCrowds = spawnerCrowds;
            _wallet = wallet;
        }

        private void StartBattle()
        {
            _spawnerCrowds.SpawnAllCrowds(_grid.transform.position, _creatureMaster.CurrentCreatures);

            _grid.SetCellsInfo();

            AllServices.Container.Get<IJoyStick>().SwitchOn();
            AllServices.Container.Get<IAudioService>().StartBattleSound();
            AllServices.Container.Get<IAdsService>().ShowBanner();

            Destroy(_grid.gameObject);
            Destroy(_creatureMaster.gameObject);
            Destroy(gameObject);
        }

        private void TryBuyMeleeCreature()
        {
            if (_grid.TryGetAvailableCell(out Cell cell))
            {
                if (_wallet.TryBuy(_priceCreature))
                {
                    _creatureMaster.CreateMeleeFirstLevel(cell);
                }
            }
        }

        private void TryBuyRangeCreature()
        {
            if (_grid.TryGetAvailableCell(out Cell cell))
            {
                if (_wallet.TryBuy(_priceCreature))
                {
                    _creatureMaster.CreateRangeFirstLevel(cell);
                }
            }
        }
    }
}
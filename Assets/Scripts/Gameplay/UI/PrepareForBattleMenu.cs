using Gameplay.Cells;
using Gameplay.Units.Crowds;
using Services;
using Services.Audio;
using Services.JoySticks;
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

        [SerializeField] private Button _startBattleButton;
        [SerializeField] private Button _buyMeleeButton;
        [SerializeField] private Button _buyRangeButton;

        private void Awake()
        {
            _startBattleButton.onClick.AddListener(StartBattle);
            _buyMeleeButton.onClick.AddListener(TryBuyMeleeCreature);
            _buyRangeButton.onClick.AddListener(TryBuyRangeCreature);
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
            Destroy(_grid.gameObject);
            Destroy(_creatureMaster.gameObject);

            AllServices.Container.Get<IJoyStick>().SwitchOn();
            gameObject.SetActive(false);

            AllServices.Container.Get<IAudioService>().PlayStartBattleSound();
        }

        private void TryBuyMeleeCreature()
        {
            AllServices.Container.Get<IAudioService>().PlayBuyCreatureSound();
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
            AllServices.Container.Get<IAudioService>().PlayBuyCreatureSound();
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
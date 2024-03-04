using Gameplay.Cells;
using Gameplay.Units.Crowds;
using Services;
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
        [SerializeField] private Button _buyCreatureButton;

        private void Awake()
        {
            _startBattleButton.onClick.AddListener(StartBattle);
            _buyCreatureButton.onClick.AddListener(TryBuyCreature);
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
            _grid.gameObject.SetActive(false);
            _creatureMaster.gameObject.SetActive(false);

            _spawnerCrowds.SpawnAllCrowds(_grid.transform.position, _creatureMaster.CurrentCreatures);

            AllServices.Container.Get<IJoyStick>().SwitchOn();

            gameObject.SetActive(false);
        }

        private void TryBuyCreature()
        {
            if (_grid.TryGetAvailableCell(out Cell cell))
            {
                if (_wallet.TryBuy(_priceCreature))
                {
                    _creatureMaster.CreateFirstLevelCreature(cell);
                }
            }
        }
    }
}
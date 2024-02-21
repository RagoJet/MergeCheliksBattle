using Gameplay.Cells;
using Services;
using UnityEngine;
using UnityEngine.UI;

namespace Gameplay.BeforeTheBattle
{
    public class PrepareForBattleMenu : MonoBehaviour
    {
        private CellGrid _grid;
        private CreatureMaster _creatureMaster;
        private Storage _storage;

        [SerializeField] private int _priceCreature = 200;

        [SerializeField] private Button _startBattleButton;
        [SerializeField] private Button _buyCreatureButton;


        private void Awake()
        {
            _startBattleButton.onClick.AddListener(StartBattle);
            _buyCreatureButton.onClick.AddListener(TryBuyCreature);
        }

        public void Construct(CellGrid grid, CreatureMaster creatureMaster)
        {
            _grid = grid;
            _creatureMaster = creatureMaster;
            _storage = AllServices.Container.Get<Storage>();
        }

        private void StartBattle()
        {
            _grid.gameObject.SetActive(false);
            _creatureMaster.gameObject.SetActive(false);
            _startBattleButton.gameObject.SetActive(false);
            _buyCreatureButton.gameObject.SetActive(false);
        }

        private void TryBuyCreature()
        {
            if (_grid.TryGetAvailableCell(out Cell cell))
            {
                if (_storage.TryBuy(_priceCreature))
                {
                    _creatureMaster.CreateFirstLevelCreature(cell);
                }
            }
        }
    }
}
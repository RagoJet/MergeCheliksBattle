using System.Collections.Generic;
using System.Linq;
using Gameplay.Cells;
using Gameplay.Units.Creatures;
using Services;
using Services.Factories;

namespace Gameplay.BeforeTheBattle
{
    public class CreaturesPool
    {
        private List<Creature> _list = new List<Creature>();

        public void AddToPool(Creature creature)
        {
            _list.Add(creature);
            creature.ReleaseCurrentCell();
            creature.gameObject.SetActive(false);
        }

        public Creature GetAndSet(int level, Cell cell)
        {
            Creature creature = _list.FirstOrDefault(x => level == x.Level);
            if (creature == null)
            {
                creature = AllServices.Container.Get<IGameFactory>().CreateCreature(level, cell);
            }


            _list.Remove(creature);

            creature.transform.position = cell.GetPosition;
            creature.SetNewCell(cell);
            creature.gameObject.SetActive(true);
            creature.Refresh();
            return creature;
        }
    }
}
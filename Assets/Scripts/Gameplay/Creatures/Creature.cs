using UnityEngine;

namespace Gameplay.Creatures
{
    [RequireComponent(typeof(Collider))]
    public class Creature : MonoBehaviour
    {
        private int _health;
        private CreatureDescription _description;
        private Collider _collider;
        private Cell _currentCell;
        public int Level => _description.Level;

        private void Awake()
        {
            _collider = GetComponent<Collider>();
        }

        public void Construct(CreatureDescription description, Cell cell)
        {
            _description = description;
            _currentCell = cell;
            cell.currentCreature = this;
        }

        public void ReturnToCell()
        {
            transform.position = _currentCell.GetPosition;
        }

        public void GetUp()
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + 4, transform.position.z);
        }

        public void SetNewCell(Cell cell)
        {
            _currentCell = cell;
            cell.currentCreature = this;
            ReturnToCell();
        }

        public void ReleaseCell()
        {
            _currentCell.currentCreature = null;
            _currentCell = null;
        }


        public void ColliderOff()
        {
            _collider.enabled = false;
        }

        public void ColliderOn()
        {
            _collider.enabled = true;
        }
    }
}
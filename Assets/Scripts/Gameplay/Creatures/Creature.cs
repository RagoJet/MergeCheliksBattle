using System;
using DG.Tweening;
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
        private Tween _tween;
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
            _tween.Kill();
            _tween = transform.DOMove(_currentCell.GetPosition, 0.5f);
        }

        public void GetUp()
        {
            _tween.Kill();
            _tween = transform.DOMoveY(_currentCell.GetPosition.y + 5, 0.3f);
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

        public void OnDisable()
        {
            _tween.Kill();
        }
    }
}
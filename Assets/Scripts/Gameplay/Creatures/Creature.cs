using System;
using DG.Tweening;
using Gameplay.Cells;
using Gameplay.Crowds;
using UnityEngine;

namespace Gameplay.Creatures
{
    public class Creature : Unit
    {
        private CreatureDescription _description;
        private Cell _currentCell;
        public Cell CurrentCell => _currentCell;
        private Tween _tween;
        public int Level => _description.Level;


        public void Construct(CreatureDescription description, Cell cell)
        {
            _description = description;
            _currentCell = cell;
            cell.currentCreature = this;
            Refresh();
        }

        public override void Refresh()
        {
            _health = _description.MaxHealth;
        }

        public void BackToCell()
        {
            _tween.Kill();
            _tween = transform.DOMove(_currentCell.GetPosition, 0.3f);
        }

        public void GetUp()
        {
            _tween.Kill();
            _tween = transform.DOMoveY(_currentCell.GetPosition.y + 5, 0.5f);
        }

        public void SetTo(Vector3 pos, Action onSetTo = null)
        {
            _tween.Kill();
            _tween = transform.DOMove(pos, 0.25f).OnComplete(() => onSetTo?.Invoke());
        }

        public void SetNewCell(Cell cell)
        {
            _currentCell = cell;
            cell.currentCreature = this;
            BackToCell();
        }

        public void ReleaseCurrentCell()
        {
            _currentCell.currentCreature = null;
            _currentCell = null;
        }

        public void OnDisable()
        {
            _tween.Kill();
        }
    }
}
using System;
using DG.Tweening;
using Gameplay.Cells;
using UnityEngine;

namespace Gameplay.Units.Creatures
{
    public class Creature : Unit
    {
        private Cell _currentCell;
        public Cell CurrentCell => _currentCell;
        private Tween _tween;

        public bool IsRange => _data.RangeAttack > 2.5f;
        public int Level => _data.Level;

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
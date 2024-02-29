using System;
using System.Collections.Generic;
using DG.Tweening;
using States;
using UnityEngine;
using Image = UnityEngine.UI.Image;

namespace Gameplay.Units.Crowds
{
    [RequireComponent(typeof(BoxCollider))]
    public class CrowdOfUnits : MonoBehaviour
    {
        protected List<Unit> units = new List<Unit>();
        protected bool fightMode;

        protected StateMachine _stateMachine = new StateMachine();
        public CrowdOfUnits targetCrowd;

        private float _timeFromLastFormat;

        [SerializeField] private Image _areaImage;
        private Tween _tween;


        private BoxCollider _collider;
        private Vector3 _sizeOfCollider;

        private List<Vector3> dotsPos = new List<Vector3>();
        private float _offset = 3;
        private int _rowCount = 4;
        private int _columnCount = 4;

        private void Awake()
        {
            _collider = GetComponent<BoxCollider>();
            _sizeOfCollider = _collider.size;
            List<Vector3> positions = new List<Vector3>();

            Vector3 startPosition = new Vector3((1 - _columnCount) * _offset / 2f, 0, (1 - _rowCount) * _offset / 2f);
            for (int i = 0; i < _rowCount; i++)
            {
                for (int j = 0; j < _columnCount; j++)
                {
                    Vector3 position = startPosition + new Vector3(j * _offset, 0, i * _offset);
                    positions.Add(position);
                }
            }

            dotsPos.Add(positions[9]);
            dotsPos.Add(positions[10]);
            dotsPos.Add(positions[5]);
            dotsPos.Add(positions[6]);
            dotsPos.Add(positions[12]);
            dotsPos.Add(positions[15]);
            dotsPos.Add(positions[0]);
            dotsPos.Add(positions[3]);
            dotsPos.Add(positions[13]);
            dotsPos.Add(positions[14]);
            dotsPos.Add(positions[1]);
            dotsPos.Add(positions[2]);
            dotsPos.Add(positions[11]);
            dotsPos.Add(positions[4]);
            dotsPos.Add(positions[7]);
            dotsPos.Add(positions[8]);
        }

        public void Construct(List<Unit> newUnits)
        {
            units = newUnits;
            foreach (var unit in newUnits)
            {
                unit.NavMeshAgentOn();
            }

            ResizeAreaImage();
        }

        public void RemoveFromCrowd(Unit unit)
        {
            units.Remove(unit);
            ResizeAreaImage();
        }

        public Unit GetOpponentFromTargetCrowd(Vector3 pos)
        {
            Unit unit = targetCrowd.GetClosestAliveOpponent(pos);

            if (unit == null)
            {
                fightMode = false;
            }

            return unit;
        }

        public Unit GetClosestAliveOpponent(Vector3 pos)
        {
            Unit AliveUnit = null;
            foreach (var unit in units)
            {
                if (unit.GetComponent<Health>().IsAlive)
                {
                    AliveUnit = unit;
                    break;
                }
            }

            if (AliveUnit == null)
            {
                return null;
            }

            float closestSqrDistance = (pos - AliveUnit.transform.position).sqrMagnitude;
            foreach (var unit in units)
            {
                if ((pos - unit.transform.position).sqrMagnitude < closestSqrDistance &&
                    unit.GetComponent<Health>().IsAlive)
                {
                    AliveUnit = unit;
                }
            }

            return AliveUnit;
        }

        public void StartFight(CrowdOfUnits crowdOfUnits)
        {
            targetCrowd = crowdOfUnits;
            fightMode = true;
            foreach (var unit in units)
            {
                unit.StartFight();
            }
        }

        private void ResizeAreaImage()
        {
            _tween.Kill();
            float xSize = units.Count * 0.2f;
            _collider.size = _sizeOfCollider * (1 + units.Count * 0.15f);
            _tween = _areaImage.rectTransform.DOScale(1 + xSize, 1f);
        }


        protected void FormatUnits()
        {
            if (Time.time - _timeFromLastFormat >= 0.3f)
            {
                for (int i = 0; i < units.Count; i++)
                {
                    units[i].GoTo(transform.TransformPoint(dotsPos[i]));
                }

                _timeFromLastFormat = Time.time;
            }
        }

        private void OnDisable()
        {
            _tween.Kill();
        }
    }
}
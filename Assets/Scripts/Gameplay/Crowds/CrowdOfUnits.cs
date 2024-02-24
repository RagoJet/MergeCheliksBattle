using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Gameplay.Crowds
{
    public class CrowdOfUnits<T> : MonoBehaviour where T : Unit
    {
        protected List<T> _units = new List<T>();
        private List<Vector3> dotsPos = new List<Vector3>();
        private float _offset = 3;
        private int _rowCount = 4;
        private int _columnCount = 4;

        private float _timeToFormat = 0.1f;

        [SerializeField] private Image _areaImage;
        private Tween _tween;


        private void Awake()
        {
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

        public void Construct(List<T> units)
        {
            _units = units;
            foreach (var unit in units)
            {
                unit.ReadyToBattle();
            }

            FormatUnits();
            ResizeAreaImage();
        }

        private void ResizeAreaImage()
        {
            if (_areaImage == null)
            {
                return;
            }

            _tween.Kill();
            float xSize = _units.Count * 0.2f;
            _tween = _areaImage.rectTransform.DOScale(1 + xSize, 1f);
        }


        protected void OnUpdate()
        {
            if (Time.time - _timeToFormat >= 0)
            {
                FormatUnits();
                _timeToFormat = Time.time;
            }
        }

        protected void FormatUnits()
        {
            for (int i = 0; i < _units.Count; i++)
            {
                _units[i].GoTo(transform.TransformPoint(dotsPos[i]));
            }
        }
    }
}
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Maryan.HeroesOfCode
{
    [CreateAssetMenu(fileName = nameof(StartPoints), menuName = EditorUtils.GridSystem + nameof(StartPoints))]
    public class StartPoints : ScriptableObject
    {
        public int Count
        {
            get { return _positions.Count; }
        }

        [SerializeField]
        private Grid _grid;
        [SerializeField]
        private List<Point> _positions;

        private List<int> _randomList = new List<int>();
        private int _lastRandomIndex = 0;

        public Point GetPointByIndex(int index)
        {
            return _positions[index];
        }

        public Vector2 GetPositionByIndex(int index)
        {
            var point = GetPointByIndex(index);
            return _grid.GetWorldPositionByPoint(point);
        }

        public Vector2 GetRandomPosition()
        {
            if(_randomList.Count == 0)
            {
                InitRandomList();
            }
            if(_lastRandomIndex > _randomList.Count - 1)
            {
                _lastRandomIndex = 0;
            }
            var position = GetPositionByIndex(_randomList[_lastRandomIndex++]);
            return position;
        }

        private void InitRandomList()
        {
            _randomList.AddRange(Enumerable.Range(0, _positions.Count - 1));
            _randomList.Shuffle();
            _lastRandomIndex = 0;
        }
    }
}

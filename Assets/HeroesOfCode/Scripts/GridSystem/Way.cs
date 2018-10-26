using UnityEngine;

namespace Maryan.HeroesOfCode
{
    [CreateAssetMenu(fileName = nameof(Way), menuName = EditorUtils.GridSystem + nameof(Way), order = 4)]
    public class Way : ScriptableObject
    {
        public Point[] Path
        {
            get
            {
                if(_path == null)
                {
                    Generate();
                }
                return _path;
            }
        }

        public Point End
        {
            get { return _endPosition; }
            set { _endPosition = value; }
        }

        public Point Start
        {
            get { return _startPosition; }
            set { _startPosition = value; }
        }

        [SerializeField]
        private Pathfinder _pathfinder;
        private Point _startPosition;
        private Point _endPosition;
        private Point[] _path;

        public void Generate()
        {
            _pathfinder.Initialize();
            _pathfinder.Find(_startPosition, _endPosition);
            _path = _pathfinder.Path;
        }

        public Vector2[] GetWorldPoints()
        {
            Vector2[] path = new Vector2[Path.Length];
            for(int i = 0; i < Path.Length; i++)
            {
                path[i] = _pathfinder.Grid.GetWorldPositionByPoint(Path[i]);
            }
            return path;
        }
    }
}

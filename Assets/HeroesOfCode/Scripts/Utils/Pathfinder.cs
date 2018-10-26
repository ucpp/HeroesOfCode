using UnityEngine;

namespace Maryan.HeroesOfCode
{
    public abstract class Pathfinder : ScriptableObject
    {
        public Grid Grid
        {
            get { return _globalGrid; }
        }

        public int PathLength
        {
            get { return _length; }
            protected set { _length = value; }
        }

        public Point[] Path
        {
            get { return _path; }
            protected set { _path = value; }
        }

        [SerializeField]
        private Grid _globalGrid;

        private int _length; // длина пути
        private Point[] _path;// координаты ячеек, входящих в путь

        public abstract void Initialize();
        public abstract bool Find(Point start, Point end);
    }
}

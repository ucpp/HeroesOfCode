using UnityEngine;

namespace Maryan.HeroesOfCode
{
    public class Enemy : MonoBehaviour
    {
        public Point Point
        {
            get { return _point; }
        }
        public Army Army
        {
            get { return _army; }
        }

        [SerializeField]
        public EnemyRuntimeSet _runtimeSet;
        [SerializeField]
        private Grid _globalGrid;
        [SerializeField]
        private Army _army;

        private Point _point;

        private void Start()
        {
            _point = _globalGrid.GetPointByWorldPosition(transform.position);
        }

        private void OnEnable()
        {
            if(_army.IsDie)
            {
                gameObject.SetActive(false);
            }
            else
            {
                _runtimeSet.Add(this);
            }
        }

        private void OnDisable()
        {
            _runtimeSet.Remove(this);
        }
    }
}

using System;
using UnityEngine;

namespace Maryan.HeroesOfCode
{
    public class PawnHeroBehaviour : MonoBehaviour
    {
        [SerializeField]
        private BaseController _controller;
        [SerializeField]
        private Hero _hero;
        [SerializeField]
        private Grid _globalGrid;
        [SerializeField]
        private Way _way;
        [SerializeField]
        private FightSearchHandler _fightSearchHandler;
        [SerializeField]
        private WayCreator _wayCreator;

        private Point _prevPoint;
        private bool _isSelectPoint;

        private void Start()
        {
            _controller.OnPressTile.AddListener(OnPressMap);
            transform.position = _hero.Position;
            var point = _globalGrid.GetPointByWorldPosition(transform.position);
            _way.Start = point;
        }

        private void OnPressMap(Point point)
        {
            var type = _globalGrid.GetTileType(point);
            if(type == Tile.Type.Impassable || _wayCreator.Way.Start == point)
            {
                _wayCreator.HideAll();
                _prevPoint = new Point(-1, -1);
                _isSelectPoint = false;
                return;
            }

            if(_prevPoint != point)
            {
                _wayCreator.Way.End = point;
                _wayCreator.Way.Generate();
                if(_wayCreator.Way.Path.Length > 0)
                {
                    _prevPoint = point;
                    _wayCreator.Create();
                    _isSelectPoint = true;
                }
            }
            else if(_prevPoint == point && _isSelectPoint)
            {
                GetComponent<PawnMover>().StartRun();
                _isSelectPoint = false;
            }
        }

        public void FindFight()
        {
            _fightSearchHandler.Find(transform.position);
        }

        private void Update()
        {
            _controller.Update();
        }

        public void UpdateHeroPosition()
        {
            _hero.Position = transform.position;
        }

        private void OnDestroy()
        {
            _controller.OnPressTile.RemoveListener(OnPressMap);
        }
    }
}
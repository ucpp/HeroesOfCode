using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Maryan.HeroesOfCode
{
    [Serializable]
    public class TileMap
    {
        public int Width
        {
            get { return _width; }
        }

        public int Height
        {
            get { return _height; }
        }
        [SerializeField]
        private int _height;

        [SerializeField]
        private int _width;

        [HideInInspector]
        [SerializeField]
        private List<Tile> _tiles;

        private bool _isInit = false;

        public TileMap() { }

        public Tile this[int x, int y]
        {
            get
            {
                CheckList();
                return _tiles[(int)y * _width + x];
            }
            set
            {
                CheckList();
                _tiles[(int)y * _width + x] = value;
            }
        }

        public int Get(int x, int y)
        {
            CheckList();
            if(y < 0 || y > _height || x < 0 || x > _width)
            {
                return (int)Tile.Type.Impassable;
            }
            return _tiles[(int)y * _width + x].Value;
        }

        private void CheckList()
        {
            if(_tiles.Count < _height * _width)
            {
                _tiles.Clear();
                _tiles.AddRange(Enumerable.Repeat(new Tile(), _height * _width));
            }
        }
    }
}

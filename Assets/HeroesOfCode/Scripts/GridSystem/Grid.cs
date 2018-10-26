using UnityEngine;

namespace Maryan.HeroesOfCode
{
    [CreateAssetMenu(fileName = nameof(Grid), menuName = EditorUtils.GridSystem + nameof(Grid))]
    public class Grid : ScriptableObject
    {
        public Vector2 StartPosition
        {
            get { return _startPosition; }
        }

        public float TileWidth
        {
            get { return _tileWidth; }
        }

        public float TileHeight
        {
            get { return _tileHeight; }
        }

        public float Width
        {
            get { return _map.Width; }
        }

        public float Height
        {
            get { return _map.Height; }
        }

        public TileMap Map
        {
            get { return _map; }
        }

        [SerializeField]
        private Vector2 _startPosition;

        [SerializeField]
        private float _tileWidth;

        [SerializeField]
        private float _tileHeight;

        [SerializeField]
        private TileMap _map;

        public Vector2 GetWorldPositionByPoint(Point point)
        {
            float x = _startPosition.x + point.x * _tileWidth + _tileWidth / 2;
            float y = _startPosition.y - point.y * _tileHeight - _tileHeight / 2;
            return new Vector2(x, y);
        }

        public Point GetPointByWorldPosition(Vector2 position)
        {
            int x = (int)((position.x - _startPosition.x) / _tileWidth);
            int y = (int)((_startPosition.y - position.y) / _tileHeight);
            return new Point(x, y);
        }

        public Tile.Type GetTileType(Point point)
        {
            return (Tile.Type)Map[point.x, point.y].Value;
        }
    }
}

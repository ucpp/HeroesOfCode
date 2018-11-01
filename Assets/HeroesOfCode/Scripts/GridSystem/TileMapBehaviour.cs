using UnityEngine;

namespace Maryan.HeroesOfCode
{
    public class TileMapBehaviour : MonoBehaviour
    {
        public bool IsEnable
        {
            get { return _isEnableEdit; }
        }

        public GridHit GridHit
        {
            get { return _gridHit; }
        }

        public Grid Grid
        {
            get { return _grid; }
        }

        [SerializeField]
        private bool _isEnableEdit;
        [SerializeField]
        private Grid _grid;
        [SerializeField]
        private GridHit _gridHit;
        [SerializeField]
        private Color _gridColor = Color.yellow;
        [SerializeField]
        private Color _impassableTilecolor = Color.red;

        private void OnDrawGizmos()
        {
            if(_isEnableEdit)
            {
                DrawGrid();
                DrawImpassibleTiles();
            }
        }

        private void DrawGrid()
        {
            Color defaultColor = Gizmos.color;
            Gizmos.color = _gridColor;

            float h = _grid.TileHeight * _grid.Height;
            float w = _grid.TileWidth * _grid.Width;
            Vector2 startPos = _grid.StartPosition;
            // горизонтальные линии
            for(int y = 0; y <= _grid.Height; y++)
            {
                Vector3 start = new Vector3(startPos.x, startPos.y - y * _grid.TileHeight, 0);
                Vector3 end = new Vector3(startPos.x + w, startPos.y - y * _grid.TileHeight, 0);
                Gizmos.DrawLine(start, end);
            }
            // вертикальные
            for(int x = 0; x <= _grid.Width; x++)
            {
                Vector3 start = new Vector3(startPos.x + x * _grid.TileWidth, startPos.y, 0);
                Vector3 end = new Vector3(startPos.x + x * _grid.TileWidth, startPos.y - h, 0);
                Gizmos.DrawLine(start, end);
            }
            Gizmos.color = defaultColor;
        }

        private void DrawImpassibleTiles()
        {
            Vector2 startPos = _grid.StartPosition;
            Color defaultColor = Gizmos.color;
            Gizmos.color = _impassableTilecolor;
            for(int y = 0; y < _grid.Height; y++)
            {
                for(int x = 0; x < _grid.Width; x++)
                {
                    if((Tile.Type)_grid.Map[x, y].Value == Tile.Type.Impassable)
                    {
                        Vector3 size = new Vector3(_grid.TileWidth, _grid.TileHeight, 0.1f);
                        float dx = startPos.x + x * _grid.TileWidth + _grid.TileWidth / 2;
                        float dy = startPos.y - y * _grid.TileHeight - _grid.TileHeight / 2;
                        Gizmos.DrawCube(new Vector3(dx, dy, -1), size);
                    }
                }
            }
            Gizmos.color = defaultColor;
        }
    }
}

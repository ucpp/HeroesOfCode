using UnityEngine;

namespace Maryan.HeroesOfCode
{
    public class GlobalGridGizmo : MonoBehaviour
    {
        [SerializeField]
        private bool _enabled;
        [SerializeField]
        private Color _gridColor = Color.yellow;
        [SerializeField]
        private Color _impassableTilecolor = Color.red;
        [SerializeField]
        private Grid _grid;

        private void OnDrawGizmos()
        {
            if(_enabled)
            {
                Gizmos.color = _gridColor;

                float h = _grid.TileHeight * _grid.Height;
                float w = _grid.TileWidth * _grid.Width;

                for(int y = 0; y <= _grid.Height; y++)
                {
                    Vector3 start = new Vector3(_grid.StartPosition.x, _grid.StartPosition.y - y * _grid.TileHeight, 0);
                    Vector3 end = new Vector3(_grid.StartPosition.x + w, _grid.StartPosition.y - y * _grid.TileHeight, 0);
                    Gizmos.DrawLine(start, end);
                }

                for(int x = 0; x <= _grid.Width; x++)
                {
                    Vector3 start = new Vector3(_grid.StartPosition.x + x * _grid.TileWidth, _grid.StartPosition.y, 0);
                    Vector3 end = new Vector3(_grid.StartPosition.x + x * _grid.TileWidth, _grid.StartPosition.y - h, 0);
                    Gizmos.DrawLine(start, end);
                }

                Gizmos.color = _impassableTilecolor;

                for(int y = 0; y < _grid.Height; y++)
                {
                    for(int x = 0; x < _grid.Width; x++)
                    {
                        if((Tile.Type)_grid.Map[x, y].Value == Tile.Type.Impassable)
                        {
                            Vector3 size = new Vector3(_grid.TileWidth, _grid.TileHeight, 0.1f);
                            float dx = _grid.StartPosition.x + x * _grid.TileWidth + _grid.TileWidth / 2;
                            float dy = _grid.StartPosition.y - y * _grid.TileHeight - _grid.TileHeight / 2;
                            Gizmos.DrawCube(new Vector3(dx, dy, -1), size);
                        }
                    }
                }
            }
        }
    }
}

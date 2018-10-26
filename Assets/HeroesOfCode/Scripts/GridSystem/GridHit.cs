using UnityEngine;

namespace Maryan.HeroesOfCode
{
    [CreateAssetMenu(fileName = nameof(GridHit), menuName = EditorUtils.GridSystem + nameof(GridHit), order = 4)]
    public class GridHit : ScriptableObject
    {
        [SerializeField]
        private Grid _grid;

        public Tile.Type GetTileType(Point point)
        {
            return (Tile.Type)_grid.Map[point.x, point.y].Value;
        }

        public Tile.Type GetTileType(Vector2 hitPosition)
        {
            var point = GetPoint(hitPosition);
            return (Tile.Type)_grid.Map[point.x, point.y].Value;
        }

        public Point GetPoint(Vector2 hitPosition)
        {
            var point = _grid.GetPointByWorldPosition(hitPosition);
            return point.Clamp(new Point(0, 0), new Point((int)_grid.Width - 1, (int)_grid.Height - 1));
        }
    }
}

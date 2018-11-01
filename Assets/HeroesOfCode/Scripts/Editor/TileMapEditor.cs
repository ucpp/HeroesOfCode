using UnityEditor;
using UnityEngine;

namespace Maryan.HeroesOfCode
{
    [CustomEditor(typeof(TileMapBehaviour))]
    public class TileMapEditor : Editor
    {
        private TileMapBehaviour Target
        {
            get { return target as TileMapBehaviour; }
        }

        private void OnSceneGUI()
        {
            if(!Target.IsEnable)
            {
                return;
            }

            int controlId = GUIUtility.GetControlID(FocusType.Passive);
            switch(Event.current.GetTypeForControl(controlId))
            {
                case EventType.MouseDown:
                    GUIUtility.hotControl = controlId;
                    if(Event.current.button == 0)
                    {
                        OnHitTile();
                    }
                    Event.current.Use();
                    break;
                case EventType.MouseUp:
                    //Возвращаем другим control'ам доступ к событиям мыши
                    GUIUtility.hotControl = 0;
                    Event.current.Use();
                    break;
            }
        }

        private void OnHitTile()
        {
            Vector3 mousePosition = Event.current.mousePosition;
            mousePosition.y = SceneView.currentDrawingSceneView.camera.pixelHeight - mousePosition.y;
            mousePosition = SceneView.currentDrawingSceneView.camera.ScreenToWorldPoint(mousePosition);
            var point = Target.GridHit.GetPoint(mousePosition);
            ChangeTileType(point);
        }

        private void ChangeTileType(Point point)
        {
            bool isEmptyTile = Target.Grid.Map[point.x, point.y].Value == (int)Tile.Type.Empty;
            Target.Grid.Map[point.x, point.y].Value = isEmptyTile ? (int)Tile.Type.Impassable : (int)Tile.Type.Empty;
        }
    }
}

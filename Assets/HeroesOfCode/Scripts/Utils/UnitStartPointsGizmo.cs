using UnityEngine;

namespace Maryan.HeroesOfCode
{
    public sealed class UnitStartPointsGizmo : MonoBehaviour
    {
        [SerializeField]
        private float _size = 0.1f;
        [SerializeField]
        private StartPoints _heroPoints;
        [SerializeField]
        private StartPoints _enemyPoints;

        private void OnDrawGizmos()
        {
            DrawPoints(_heroPoints);
            DrawPoints(_enemyPoints);
        }

        private void DrawPoints(StartPoints startPoints)
        {
            for(int i = 0; i < startPoints.Count; i++)
            {
                Gizmos.DrawSphere(startPoints.GetPositionByIndex(i), _size);
            }
        }
    }
}

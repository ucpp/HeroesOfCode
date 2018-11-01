using UnityEngine;

namespace Maryan.HeroesOfCode
{
    [CreateAssetMenu(fileName = nameof(FightSearchHandler), menuName = EditorUtils.GridSystem + nameof(FightSearchHandler))]
    public sealed class FightSearchHandler : ScriptableObject
    {
        [SerializeField]
        private Grid _globalGrid;
        [SerializeField]
        private EnemyRuntimeSet _enemyRuntimeSet;
        [SerializeField]
        private GameEvent _onFindFight;
        [SerializeField]
        private BattleBehaviour _computerBattleBehaviour;

        public void Find(Vector2 position)
        {
            if(IsFindFight(position))
            {
                _onFindFight.Raise();
            }
        }

        private bool IsFindFight(Vector2 position)
        {
            var point = _globalGrid.GetPointByWorldPosition(position);
            for(int i = 0; i < _enemyRuntimeSet.Items.Count; i++)
            {
                var enemyPoint = _enemyRuntimeSet.Items[i].Point;
                if(Mathf.Abs(enemyPoint.x - point.x) <= 1 && Mathf.Abs(enemyPoint.y - point.y) <= 1)
                {
                    _computerBattleBehaviour.SetArmy(_enemyRuntimeSet.Items[i].Army, false);
                    return true;
                }
            }
            return false;
        }
    }
}
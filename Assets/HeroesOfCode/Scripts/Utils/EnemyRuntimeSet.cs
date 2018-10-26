using UnityEngine;

namespace Maryan.HeroesOfCode
{
    [CreateAssetMenu(fileName = nameof(EnemyRuntimeSet), menuName = EditorUtils.UtilsMenu + nameof(EnemyRuntimeSet), order = 0)]
    public class EnemyRuntimeSet : RuntimeSet<Enemy> { }
}
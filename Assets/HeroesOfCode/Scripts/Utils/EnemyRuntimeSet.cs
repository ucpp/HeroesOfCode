using UnityEngine;

namespace Maryan.HeroesOfCode
{
    [CreateAssetMenu(fileName = nameof(EnemyRuntimeSet), menuName = EditorUtils.UtilsMenu + nameof(EnemyRuntimeSet))]
    public sealed class EnemyRuntimeSet : RuntimeSet<Enemy> { }
}
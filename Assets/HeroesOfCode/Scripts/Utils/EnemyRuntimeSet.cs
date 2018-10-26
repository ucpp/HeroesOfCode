using UnityEngine;

namespace Maryan.HeroesOfCode
{
    [CreateAssetMenu(fileName = nameof(EnemyRuntimeSet), menuName = EditorUtils.UtilsMenu + nameof(EnemyRuntimeSet))]
    public class EnemyRuntimeSet : RuntimeSet<Enemy> { }
}
using UnityEngine;

namespace Maryan.HeroesOfCode
{
    [CreateAssetMenu(fileName = nameof(SquadRuntimeSet), menuName = EditorUtils.UtilsMenu + nameof(SquadRuntimeSet))]
    public class SquadRuntimeSet : RuntimeSet<SquadBehaviour>
    {
        public SquadBehaviour GetUnitByPoint(Point point)
        {
            return Items.Find(unit => unit.Position == point);
        }
    }
}

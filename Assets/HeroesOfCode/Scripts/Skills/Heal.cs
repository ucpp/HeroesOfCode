using UnityEngine;

namespace Maryan.HeroesOfCode
{
    [CreateAssetMenu(fileName = nameof(Heal), menuName = EditorUtils.SkillsMenu + nameof(Heal), order = 0)]
    public class Heal : Skill
    {
        [Tooltip("Healing power as a percentage of maximum health.")]
        [Range(0, 100)]
        [SerializeField]
        private int _forceInPercentages;

        public override void Cast()
        {
            foreach(var unit in UnitRuntimeSet.Items)
            {
                if(unit.IsOwn && !unit.IsDie)
                {
                    unit.GetHeal(_forceInPercentages);
                }
            }
        }
    }
}

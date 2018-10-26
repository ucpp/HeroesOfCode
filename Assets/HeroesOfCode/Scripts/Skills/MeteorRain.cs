using UnityEngine;

namespace Maryan.HeroesOfCode
{
    [CreateAssetMenu(fileName = nameof(MeteorRain), menuName = EditorUtils.SkillsMenu + nameof(MeteorRain), order = 0)]
    public class MeteorRain : Skill
    {
        [Tooltip("Damage force to all units.")]
        [SerializeField]
        private int _force;

        public override void Cast()
        {
            foreach(var unit in UnitRuntimeSet.Items)
            {
                if(!unit.IsDie)
                {
                    unit.GetAttack(_force);
                }
            }
        }
    }
}

using UnityEngine;

namespace Maryan.HeroesOfCode
{
    [CreateAssetMenu(fileName = nameof(Frenzy), menuName = EditorUtils.SkillsMenu + nameof(Frenzy))]
    public class Frenzy : Skill, ITargetable<SquadBehaviour>, IPersonable<SquadBehaviour>
    {
        [Tooltip("Base force (C).")]
        [SerializeField]
        private int _baseForce;

        [Tooltip("frenzy ratio (k).")]
        [SerializeField]
        private int _ratio;

        public SquadBehaviour Target { get; set; }
        public SquadBehaviour Owner { get; set; }

        public override void Cast()
        {
            if(Target != null && Owner != null)
            {
                var force = _baseForce + Owner.TotalDamagePerBattle * _ratio;
                Target.GetHit(force);
                Target = null;
                Owner = null;
            }
        }
    }
}

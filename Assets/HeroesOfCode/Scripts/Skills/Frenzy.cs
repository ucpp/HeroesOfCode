using UnityEngine;

namespace Maryan.HeroesOfCode
{
    [CreateAssetMenu(fileName = nameof(Frenzy), menuName = EditorUtils.SkillsMenu + nameof(Frenzy), order = 0)]
    public class Frenzy : Skill
    {
        [Tooltip("Base force (C).")]
        [SerializeField]
        private int _baseForce;

        [Tooltip("frenzy ratio (k).")]
        [SerializeField]
        private int _ratio;

        public override void Cast()
        {
        }
    }
}

using UnityEngine;

namespace Maryan.HeroesOfCode
{
    public abstract class Skill : ScriptableObject
    {
        public Sprite Icon
        {
            get { return _icon; }
        }

        protected UnitRuntimeSet UnitRuntimeSet
        {
            get { return _unitRuntimeSet; }
        }

        [SerializeField]
        private UnitRuntimeSet _unitRuntimeSet;
        [SerializeField]
        private string _name;
        [SerializeField]
        private Sprite _icon;

        public abstract void Cast();
    }
}

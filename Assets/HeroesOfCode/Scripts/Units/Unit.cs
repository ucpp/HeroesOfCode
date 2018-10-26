using UnityEngine;

namespace Maryan.HeroesOfCode
{
    [CreateAssetMenu(fileName = nameof(Unit), menuName = EditorUtils.PawnsMenu + nameof(Unit), order = 0)]
    public class Unit : ScriptableObject
    {
        public GameObject Prefab
        {
            get { return _prefab; }
        }
        public int ForceAttack
        {
            get { return _forceAttack; }
        }
        public int Health
        {
            get { return _maxHealth; }
        }
        public Skill Skill
        {
            get { return _skill;  }
        }

        [SerializeField]
        private int _maxHealth;
        [SerializeField]
        private int _forceAttack;
        [SerializeField]
        private GameObject _prefab;
        [SerializeField]
        private Skill _skill;
    }
}

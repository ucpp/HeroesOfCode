using UnityEngine;

namespace Maryan.HeroesOfCode
{
    public class SquadBehaviour : MonoBehaviour
    {
        public Unit Unit
        {
            get { return _squad.Unit; }
        }

        public int Count
        {
            get
            {
                if(_squad == null)
                {
                    return 0;
                }
                return _health / _squad.Unit.Health;
            }
        }

        public int AttackForce
        {
            get { return Count * _squad.Unit.ForceAttack; }
        }

        // нанесенный урон юнитом "с руки"
        public int TotalDamagePerBattle
        {
            get;
            set;
        }

        public int Health
        {
            get { return _health; }
            private set
            {
                _health = value;
                _healthBar.SetValue(_health);
            }
        }

        public bool IsOwn
        {
            get { return _isOwn; }
            private set { _isOwn = value; }
        }

        public bool IsDie
        {
            get { return _health <= 0; }
        }

        public Point Position
        {
            get { return _position; }
            set { _position = value; }
        }

        [SerializeField]
        public SquadRuntimeSet _runtimeSet;
        [SerializeField]
        private UnitModificator _selectModificator;
        [SerializeField]
        private Transform _objectContainer;
        [SerializeField]
        private HealthBar _healthBar;
        
        private bool _isOwn;
        private Point _position;
        private SquadData _squad;
        private int _health;
        private int _startCount;

        public void Init(SquadData squad, bool isOwn)
        {
            SetSquad(squad);
            IsOwn = isOwn;
            bool isRightDirection = isOwn;
            SetDirection(isRightDirection);
            TotalDamagePerBattle = 0;
        }

        private void SetSquad(SquadData squad)
        {
            if(squad != null)
            {
                _startCount = squad.CurrentCount;
                _squad = squad;
                Health = squad.Unit.Health * squad.CurrentCount;
            }
        }

        private void SetDirection(bool right)
        {
            var scale = _objectContainer.localScale;
            var direction = right ? 1 : -1;
            _objectContainer.localScale = new Vector3(scale.x * direction, scale.y, scale.z);
        }

        public void InitializeSkill()
        {
            if(_squad.Unit.Skill != null)
            {
                var skill = _squad.Unit.Skill as IPersonable<SquadBehaviour>;
                if(skill != null)
                {
                    skill.Owner = this;
                }
            }
        }

        public void Select()
        {
            _selectModificator.StartExecute(transform);
        }

        public void UnSelect()
        {
            _selectModificator.Stop(transform);
        }

        public void GetHit(int force)
        {
            Health -= force;
            var maxHealth = _startCount * _squad.Unit.Health;
            Health = Mathf.Clamp(_health, 0, maxHealth);
            _squad.CurrentCount = Count;
            if(Health == 0)
            {
                Die();
            }
        }

        public void GetHeal(int healPercent)
        {
            var maxHealth = _startCount * _squad.Unit.Health;
            var value = maxHealth * healPercent / 100;
            Health += value;
            Health = Mathf.Clamp(Health, 0, maxHealth);
        }

        private void OnEnable()
        {
            _runtimeSet.Add(this);
        }

        private void OnDisable()
        {
            _runtimeSet.Remove(this);
        }

        private void Die()
        {
            gameObject.SetActive(false);
        }
    }
}
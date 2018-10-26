using UnityEngine;

namespace Maryan.HeroesOfCode
{
    public class UnitBehaviour : MonoBehaviour
    {
        public bool IsOwn
        {
            get { return _isOwn; }
            set { _isOwn = value; }
        }

        public Point Position
        {
            get { return _position; }
            set { _position = value; }
        }

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

        public int Health
        {
            get { return _health; }
            private set
            {
                _health = value;
                _healthBar.SetValue(_health);
            }
        }

        public bool IsDie
        {
            get { return _health <= 0; }
        }

        [SerializeField]
        public UnitRuntimeSet _runtimeSet;
        [SerializeField]
        private UnitModificator _selectModificator;
        [SerializeField]
        private HealthBar _healthBar;
        
        private bool _isOwn;
        private Point _position;
        private SquadData _squad;
        private int _health;
        private int _startCount;

        public void SetSquad(SquadData squad)
        {
            if(squad != null)
            {
                _startCount = squad.CurrentCount;
                _squad = squad;
                Health = squad.Unit.Health * squad.CurrentCount;
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

        public void GetAttack(int force)
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
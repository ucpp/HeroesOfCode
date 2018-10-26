using System;
using UnityEngine;

namespace Maryan.HeroesOfCode
{
    [CreateAssetMenu(fileName = nameof(ComputerBattleBehaviour), menuName = EditorUtils.UtilsMenu + nameof(ComputerBattleBehaviour))]
    public class ComputerBattleBehaviour : BattleBehaviour
    {
        public override Army Army
        {
            get { return _army; }
            protected set { _army = value; }
        }

        [SerializeField]
        private UnitRuntimeSet _unitRuntimeSet;
        [SerializeField]
        private float _timeToStartAttack;
        [NonSerialized]
        private Army _army;
        [NonSerialized]
        private float _dt = 0;

        public override void Update()
        {
            if(IsAttacking)
            {
                _dt += Time.deltaTime;
                if(_dt > _timeToStartAttack)
                {
                    var oppositeSquad = GetRandomSquad();
                    var attack = ActiveSquad.AttackForce;
                    oppositeSquad.GetHit(attack);
                    EndAttack();
                }
            }
        }

        protected override void EndAttack()
        {
            base.EndAttack();
            _dt = 0;
        }

        private UnitBehaviour GetRandomSquad()
        {
            var oppositeUnits = _unitRuntimeSet.Items.FindAll(unit => unit.IsOwn && !unit.IsDie);
            if(oppositeUnits.Count > 0)
            {
                return oppositeUnits[UnityEngine.Random.Range(0, oppositeUnits.Count)];
            }
            return null;
        }
    }
}
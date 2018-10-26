using UnityEngine;

namespace Maryan.HeroesOfCode
{
    [CreateAssetMenu(fileName = nameof(PlayerBattleBehaviour), menuName = EditorUtils.UtilsMenu + nameof(PlayerBattleBehaviour))]
    public class PlayerBattleBehaviour : BattleBehaviour
    {
        public override Army Army
        {
            get { return _army; }
            protected set { _army = value; }
        }

        [SerializeField]
        private Army _army;
        [SerializeField]
        private BaseController _controller;
        [SerializeField]
        private GuiController _guiController;
        [SerializeField]
        private UnitRuntimeSet _unitRuntimeSet;

        public override void Initialize()
        {
            if(Army == null)
            {
                return;
            }
            base.Initialize();

            _controller.OnPressTile.AddListener(OnPressTile);
            _guiController.OnPressSkill.AddListener(OnPressSkill);
        }

        private void OnPressTile(Point point)
        {
            var unit = _unitRuntimeSet.GetUnitByPoint(point);
            if(unit != null && !unit.IsOwn)
            {
                var attack = ActiveSquad.AttackForce;
                unit.GetAttack(attack);
                EndAttack();
            }
        }

        private void OnPressSkill()
        {
            if(ActiveSquad.Unit.Skill != null)
            {
                ActiveSquad.Unit.Skill.Cast();
                EndAttack();
            }
        }

        public override void StartAttack(Army oppositeArmy)
        {
            base.StartAttack(oppositeArmy);
            _controller.IsEnable = true;
        }

        protected override void EndAttack()
        {
            base.EndAttack();
            _controller.IsEnable = false;
        }

        public override void Update()
        {
            _controller.Update();
        }

        public override void EndBattle()
        {
            _controller.Clear();
            _guiController.Clear();
        }
    }
}
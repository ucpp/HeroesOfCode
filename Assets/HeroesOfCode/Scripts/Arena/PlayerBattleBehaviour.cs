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
        private SquadRuntimeSet _unitRuntimeSet;

        private bool _isActivateTargetSkill = false;

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
                if(_isActivateTargetSkill)
                {
                    _isActivateTargetSkill = false;
                    CastTargetableSkill(unit);
                }
                else
                {
                    unit.GetHit(attack);
                }
                ActiveSquad.TotalDamagePerBattle += attack;
                EndAttack();
            }
        }

        private void CastTargetableSkill(SquadBehaviour target)
        {
            ActiveSquad.InitializeSkill();
            var targetableSkill = ActiveSquad.Unit.Skill as ITargetable<SquadBehaviour>;
            if(targetableSkill != null)
            {
                targetableSkill.Target = target;
                ActiveSquad.Unit.Skill.Cast();
            }
        }

        private void OnPressSkill()
        {
            if(ActiveSquad.Unit.Skill != null)
            {
                var targetableSkill = ActiveSquad.Unit.Skill as ITargetable<SquadBehaviour>;
                if(targetableSkill == null)
                {
                    ActiveSquad.Unit.Skill.Cast();
                    EndAttack();
                }
                else
                {
                    _isActivateTargetSkill = true;
                }
            }
        }

        public override void StartAttack()
        {
            base.StartAttack();
            _controller.IsEnable = true;
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

        protected override void EndAttack()
        {
            base.EndAttack();
            _controller.IsEnable = false;
        }
    }
}
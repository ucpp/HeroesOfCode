using UnityEngine;

namespace Maryan.HeroesOfCode
{
    public sealed class BattleGui : MonoBehaviour
    {
        [SerializeField]
        private PlayerBattleBehaviour _playerBattleBehaviour;
        [SerializeField]
        private SkillButton _activeSkillButton;
        [SerializeField]
        private GuiController _guiController;

        private void Start()
        {
            OnChangeState();
            _playerBattleBehaviour.OnStartAttack.AddListener(OnChangeState);
            _playerBattleBehaviour.OnEndAttack.AddListener(HideActiveSkillButton);
        }

        private void OnChangeState()
        {
            if(_playerBattleBehaviour.ActiveSquad != null && _playerBattleBehaviour.ActiveSquad.Unit.Skill != null)
            {
                InitializeActiveSkillButton();
            }
            else
            {
                HideActiveSkillButton();
            }
        }

        private void HideActiveSkillButton()
        {
            _activeSkillButton.gameObject.SetActive(false);
            _activeSkillButton.OnPress.RemoveAllListeners();
        }

        private void InitializeActiveSkillButton()
        {
            var skill = _playerBattleBehaviour.ActiveSquad.Unit.Skill;
            _activeSkillButton.gameObject.SetActive(true);
            _activeSkillButton.OnPress.AddListener(OnPreesSkill);
            _activeSkillButton.SetIcon(skill.Icon);
        }

        private void OnPreesSkill()
        {
            _guiController.OnPressSkill.Invoke();
            HideActiveSkillButton();
        }
    }
}
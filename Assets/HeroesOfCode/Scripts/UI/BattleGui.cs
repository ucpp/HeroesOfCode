using UnityEngine;

namespace Maryan.HeroesOfCode
{
    public class BattleGui : MonoBehaviour
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
            if(_playerBattleBehaviour.ActiveSquad != null)
            {
                var skill = _playerBattleBehaviour.ActiveSquad.Unit.Skill;
                if(skill == null)
                {
                    HideActiveSkillButton();
                }
                else
                {
                    _activeSkillButton.gameObject.SetActive(true);
                    _activeSkillButton.OnPress.AddListener(
                        ()=> {
                            _guiController.OnPressSkill.Invoke();
                            HideActiveSkillButton();
                        });
                    _activeSkillButton.SetIcon(skill.Icon);
                }
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
    }
}
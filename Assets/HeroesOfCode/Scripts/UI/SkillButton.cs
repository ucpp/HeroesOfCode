using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Maryan.HeroesOfCode
{
    [RequireComponent(typeof(Button))]
    public sealed class SkillButton : MonoBehaviour
    {
        public UnityEvent OnPress
        {
            get
            {
                if(_button == null)
                {
                    _button = GetComponent<Button>();
                }
                return _button.onClick;
            }
        }

        [SerializeField]
        private Image _iconRenderer;
        private Button _button;

        public void SetIcon(Sprite icon)
        {
            _iconRenderer.sprite = icon;
        }
    }
}

using System;
using UnityEngine;
using UnityEngine.Events;

namespace Maryan.HeroesOfCode
{
    [CreateAssetMenu(fileName = nameof(GuiController), menuName = EditorUtils.UtilsMenu + nameof(GuiController))]
    public class GuiController : ScriptableObject
    {
        public UnityEvent OnPressSkill
        {
            get { return _onPressSkill; }
        }

        [NonSerialized]
        private UnityEvent _onPressSkill = new UnityEvent();

        public void Clear()
        {
            _onPressSkill.RemoveAllListeners();
        }
    }
}

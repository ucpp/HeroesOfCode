using UnityEngine;
using UnityEngine.UI;

namespace Maryan.HeroesOfCode
{
    public sealed class HealthBar : MonoBehaviour
    {
        [SerializeField]
        private Text _label;

        public void SetValue(int health)
        {
            _label.text = health.ToString();
        }
    }
}

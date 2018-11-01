using UnityEngine;

namespace Maryan.HeroesOfCode
{
    [CreateAssetMenu(fileName = nameof(ScenesList), menuName = EditorUtils.UtilsMenu + nameof(ScenesList))]
    public sealed class ScenesList : ScriptableObject
    {
        public string FightSceneName
        {
            get { return _fightSceneName; }
        }

        public string MapSceneName
        {
            get { return _globalMapSceneName; }
        }

        [SerializeField]
        private string _fightSceneName;
        [SerializeField]
        private string _globalMapSceneName;
    }
}

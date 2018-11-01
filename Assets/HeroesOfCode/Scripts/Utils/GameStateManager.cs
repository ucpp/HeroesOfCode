using UnityEngine;
using UnityEngine.SceneManagement;

namespace Maryan.HeroesOfCode
{
    public sealed class GameStateManager : MonoBehaviour
    {
        [SerializeField]
        private ScenesList _scenes;

        public void LoadArena()
        {
            SceneManager.LoadScene(_scenes.FightSceneName);
        }

        public void LoadMap()
        {
            SceneManager.LoadScene(_scenes.MapSceneName);
        }
    }
}

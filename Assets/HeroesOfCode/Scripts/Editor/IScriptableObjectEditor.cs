using UnityEngine;

namespace Maryan.HeroesOfCode
{
    public interface IScriptableObjectEditor<out T> where T : ScriptableObject
    {
        string Name { get; }
        void Init();
        void Draw();
    }
}

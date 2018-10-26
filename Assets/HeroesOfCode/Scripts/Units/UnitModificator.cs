using UnityEngine;

namespace Maryan.HeroesOfCode
{
    public abstract class UnitModificator : ScriptableObject
    {
        public abstract void StartExecute(Transform transform);
        public abstract void Stop(Transform transform);
    }
}

using System;
using UnityEngine;

namespace Maryan.HeroesOfCode
{
    [Serializable]
    public class SquadData
    {
        public Unit Unit
        {
            get { return _unit; }
        }
        public int CurrentCount
        {
            get { return _currentCount; }
            set { _currentCount = value; }
        }
        public int Count
        {
            get { return _count; }
        }

        [SerializeField]
        private Unit _unit;
        [SerializeField]
        private int _count;
        [NonSerialized]
        private int _currentCount;

        public void Init()
        {
            _currentCount = _count;
        }
    }
}
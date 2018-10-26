using System;
using System.Collections.Generic;
using UnityEngine;

namespace Maryan.HeroesOfCode
{
    [CreateAssetMenu(fileName = nameof(Army), menuName = EditorUtils.PawnsMenu + nameof(Army), order = 0)]
    public class Army : ScriptableObject
    {
        public IEnumerable<SquadData> Squads
        {
            get { return _squads; }
        }
        public int Count
        {
            get { return _squads.Count; }
        }
        public bool IsDie
        {
            set { _isDie = value; }
            get { return _isDie; }
        }

        [SerializeField]
        private List<SquadData> _squads;
        [NonSerialized]
        private bool _isDie = false;

        private int _squadIndex = 0;

        private void OnEnable()
        {
            hideFlags = HideFlags.DontUnloadUnusedAsset;
            foreach(var squad in _squads)
            {
                squad.Init();
            }
        }
    }
}

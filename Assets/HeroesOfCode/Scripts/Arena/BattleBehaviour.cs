using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Maryan.HeroesOfCode
{
    public abstract class BattleBehaviour : ScriptableObject
    {
        public UnityEvent OnEndAttack
        {
            get { return _onEndAttack; }
        }

        public UnityEvent OnStartAttack
        {
            get { return _onStartAttack; }
        }

        public abstract Army Army
        {
            get;
            protected set;
        }

        public SquadBehaviour ActiveSquad
        {
            get { return _activeSquad; }
        }

        protected bool IsAttacking
        {
            get { return _isAttacking; }
        }

        public bool IsDie
        {
            get { return _units.Count == 0; }
        }
        
        [SerializeField]
        private StartPoints _startPoints;

        [NonSerialized]
        private UnityEvent _onEndAttack = new UnityEvent();
        [NonSerialized]
        private UnityEvent _onStartAttack = new UnityEvent();
        [NonSerialized]
        private List<SquadBehaviour> _units = new List<SquadBehaviour>();
        [NonSerialized]
        private SquadBehaviour _activeSquad;
        [NonSerialized]
        private bool _isOwn = true;
        [NonSerialized]
        private bool _isAttacking;
        [NonSerialized]
        private int _activeSquadIndex = 0;

 
        public void SetArmy(Army army, bool isOwn)
        {
            Army = army;
            _isOwn = isOwn;
        }

        public virtual void Initialize()
        {
            if(Army == null)
            {
                Debug.Log("Army is null!");
                return;
            }

            _activeSquadIndex = 0;
            var armyLoader = new ArmyLoader();
            _units = armyLoader.Load(Army, _startPoints, _isOwn);
        }

        public virtual void StartAttack()
        {
            _isAttacking = true;
            _activeSquad = GetNextSquad();
            _activeSquad.Select();
            _onStartAttack.Invoke();
        }

        public void CheckState()
        {
            ClearDeadsFromList();
            Army.IsDie = IsDie;
        }

        public virtual void Update() { }

        public virtual void EndBattle() { }

        protected virtual void EndAttack()
        {
            _isAttacking = false;
            _activeSquad.UnSelect();
            _activeSquad = null;
            _onEndAttack.Invoke();
        }

        private void OnEnable()
        {
            hideFlags = HideFlags.DontUnloadUnusedAsset;
        }

        private SquadBehaviour GetNextSquad()
        {
            ClearDeadsFromList();

            if(_units.Count == 0)
            {
                return null;
            }

            var result = _units[_activeSquadIndex];
            _activeSquadIndex++;
            if(_activeSquadIndex > _units.Count - 1)
            {
                _activeSquadIndex = 0;
            }
            return result;
        }

        private void ClearDeadsFromList()
        {
            for(int i = _units.Count - 1; i >= 0; i--)
            {
                if(_units[i].IsDie)
                {
                    _units.RemoveAt(i);
                }
            }
            ClampActiveSquadIndex();
        }

        private void ClampActiveSquadIndex()
        {
            _activeSquadIndex = Mathf.Clamp(_activeSquadIndex, 0, _units.Count - 1);
        }
    }
}
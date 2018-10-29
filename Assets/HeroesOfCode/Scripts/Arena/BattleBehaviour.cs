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

        public UnitBehaviour ActiveSquad
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

        [NonSerialized]
        protected Army oppositeArmy;
        [SerializeField]
        private StartPoints _startPoints;
        [NonSerialized]
        private List<UnitBehaviour> _units = new List<UnitBehaviour>();
        [NonSerialized]
        private bool _isOwn = true;
        [NonSerialized]
        private bool _isAttacking;
        [NonSerialized]
        private UnityEvent _onEndAttack = new UnityEvent();
        [NonSerialized]
        private UnityEvent _onStartAttack = new UnityEvent();
        [NonSerialized]
        private int _activeSquadIndex = 0;
        [NonSerialized]
        private UnitBehaviour _activeSquad;

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
            int squadIndex = 0;
            _units.Clear();
            foreach(var squad in Army.Squads)
            {
                if(squad.CurrentCount == 0)
                {
                    continue;
                }
                var unit = Instantiate(squad.Unit.Prefab);
                var unitBehaviour = unit.GetComponent<UnitBehaviour>();
                unitBehaviour.Init(squad, _isOwn);
                unitBehaviour.Position = _startPoints.GetPointByIndex(squadIndex);
                unit.transform.position = _startPoints.GetPositionByIndex(squadIndex);
                _units.Add(unitBehaviour);
                squadIndex++;
            }
        }

        private void OnEnable()
        {
            hideFlags = HideFlags.DontUnloadUnusedAsset;
        }

        public virtual void StartAttack(Army oppositeArmy)
        {
            _isAttacking = true;
            _activeSquad = GetNextSquad();
            _activeSquad.Select();
            this.oppositeArmy = oppositeArmy;
            _onStartAttack.Invoke();
        }

        protected virtual void EndAttack()
        {
            _isAttacking = false;
            _activeSquad.UnSelect();
            _activeSquad = null;
            oppositeArmy = null;
            _onEndAttack.Invoke();
        }

        public virtual void Update() { }

        public UnitBehaviour GetNextSquad()
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

        public void CheckState()
        {
            ClearDeadsFromList();
            Army.IsDie = IsDie;
        }

        public virtual void EndBattle() { } 
    }
}
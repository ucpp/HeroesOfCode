using System;
using UnityEngine;
using UnityEngine.Events;

namespace Maryan.HeroesOfCode
{
    [CreateAssetMenu(fileName = nameof(BaseController), menuName = EditorUtils.UtilsMenu + nameof(BaseController), order = 0)]
    public class BaseController : ScriptableObject
    {
        public bool IsEnable
        {
            get { return _isEnable; }
            set { _isEnable = value; }
        }

        public PressTileEvent OnPressTile
        {
            get { return _onPressTile; }
        }

        [SerializeField]
        private GridHit _gridHit;
        [NonSerialized]
        private bool _isEnable = true;
        [NonSerialized]
        private PressTileEvent _onPressTile = new PressTileEvent();
        
        public void Update()
        {
            if(!IsEnable)
            {
                return;
            }

            if(IsPress())
            {
                var hitPosition = GetHitPosition();
                var point = _gridHit.GetPoint(hitPosition);
                _onPressTile.Invoke(point);
            }
        }

        public virtual bool IsPress()
        {
            return Input.GetMouseButtonDown(0);
        }

        public Vector2 GetHitPosition()
        {
            return Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }

        public void Clear()
        {
            _onPressTile.RemoveAllListeners();
            _isEnable = true;
        }
    }

    [Serializable]
    public class PressTileEvent : UnityEvent<Point> { }
}

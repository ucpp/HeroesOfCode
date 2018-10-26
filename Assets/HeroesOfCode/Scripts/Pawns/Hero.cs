using System;
using UnityEngine;

namespace Maryan.HeroesOfCode
{
    [CreateAssetMenu(fileName = nameof(Hero), menuName = EditorUtils.PawnsMenu + nameof(Hero), order = 0)]
    public class Hero : ScriptableObject
    {
        public Vector2 Position
        {
            get { return _position; }
            set { _position = value; }
        }

        [SerializeField]
        private Army _army;
        [SerializeField]
        private StartPoints _startPoints;
        [NonSerialized]
        private Vector2 _position;
        [NonSerialized]
        private bool _isInitializePosition = false;

        private void OnEnable()
        {
            hideFlags = HideFlags.DontUnloadUnusedAsset;
            if(!_isInitializePosition)
            {
                _isInitializePosition = true;
                _position = _startPoints.GetRandomPosition();
            }
        }
    }
}
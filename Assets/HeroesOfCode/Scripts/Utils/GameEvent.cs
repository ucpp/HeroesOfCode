﻿using System.Collections.Generic;
using UnityEngine;

namespace Maryan.HeroesOfCode
{
    [CreateAssetMenu(fileName = nameof(GameEvent), menuName = EditorUtils.UtilsMenu + nameof(GameEvent), order = 0)]
    public class GameEvent : ScriptableObject
    {
        private readonly List<GameEventListener> _eventListeners = new List<GameEventListener>();

        public void Raise()
        {
            for(int i = _eventListeners.Count - 1; i >= 0; i--)
                _eventListeners[i].OnEventRaised();
        }

        public void RegisterListener(GameEventListener listener)
        {
            if(!_eventListeners.Contains(listener))
                _eventListeners.Add(listener);
        }

        public void UnregisterListener(GameEventListener listener)
        {
            if(_eventListeners.Contains(listener))
                _eventListeners.Remove(listener);
        }
    }
}
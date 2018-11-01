using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Maryan.HeroesOfCode
{
    public sealed class GameEventsSetListener : MonoBehaviour
    {
        [SerializeField]
        private List<GameEventListener> _listeners;

        public void OnEnable()
        {
            foreach(var listener in _listeners)
            {
                listener.OnEnable();
            }
        }

        public void OnDisable()
        {
            foreach(var listener in _listeners)
            {
                listener.OnDisable();
            }
        }
    }

    [Serializable]
    public sealed class GameEventListener
    {
        [Tooltip("Event to register with.")]
        [SerializeField]
        public GameEvent _event;

        [Tooltip("Response to invoke when Event is raised.")]
        [SerializeField]
        public UnityEvent _response;

        public void OnEnable()
        {
            _event.RegisterListener(this);
        }

        public void OnDisable()
        {
            _event.UnregisterListener(this);
        }

        public void OnEventRaised()
        {
            _response.Invoke();
        }
    }
}
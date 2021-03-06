﻿using System;
using System.Collections.Generic;
using UnityEngine;

namespace Maryan.HeroesOfCode
{
    [CreateAssetMenu(fileName = nameof(WayCreator), menuName = EditorUtils.GridSystem + nameof(WayCreator))]
    public sealed class WayCreator : ScriptableObject
    {
        public Way Way
        {
            get { return _way; }
        }
        [SerializeField]
        private Way _way;
        [SerializeField]
        private GameObject _etalonDot;
        [NonSerialized]
        private List<Dot> _dots = new List<Dot>();
        [NonSerialized]
        private GameObject _dotsContainer;

        public void Create()
        {
            _way.Generate();
            var worldPositions = _way.GetWorldPoints();
            var points = _way.Path;
            if(_dotsContainer == null)
            {
                _dotsContainer = new GameObject(nameof(_dotsContainer));
            }
            while(_dots.Count < points.Length)
            {
                _dots.Add(new Dot(Instantiate(_etalonDot), _dotsContainer));
            }
            for(int i = 0; i < worldPositions.Length; i++)
            {
                _dots[i].GameObject.SetActive(true);
                _dots[i].GameObject.transform.position = worldPositions[i];
                _dots[i].Point = points[i];
            }
            for(int i = worldPositions.Length; i < _dots.Count; i++)
            {
                _dots[i].GameObject.SetActive(false);
            }
        }

        public void HideDot(Point point)
        {
            var findDot = _dots.Find(dot => dot.Point == point);
            if(findDot != null)
            {
                findDot.GameObject.SetActive(false);
            }
        }

        public void HideAll()
        {
            for(int i = 0; i < _dots.Count; i++)
            {
                _dots[i].GameObject.SetActive(false);
            }
        }

        public void Clear()
        {
            _dots.Clear();
        }

        public sealed class Dot
        {
            public Point Point { set; get; }
            public GameObject GameObject { set; get; }

            public Dot() { }

            public Dot(GameObject gameObject, GameObject container = null)
            {
                GameObject = gameObject;
                if(container != null)
                {
                    GameObject.transform.parent = container.transform;
                }
            }
        }
    }
}

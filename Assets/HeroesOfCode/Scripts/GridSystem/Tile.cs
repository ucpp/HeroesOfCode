using System;
using UnityEngine;

namespace Maryan.HeroesOfCode
{
    [Serializable]
    public class Tile
    {
        public enum Type : int
        {
            Empty = 0,
            Impassable = 1
        }

        public int Value
        {
            get { return (int)_type; }
            set { _type = (Type)value; }
        }

        [SerializeField]
        private Type _type;
    }
}

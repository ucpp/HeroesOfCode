using System;
using UnityEngine;

namespace Maryan.HeroesOfCode
{
    [Serializable]
    public class Tile
    {
        public enum Type : int
        {
            Empty = 0, //проходимая клетка
            Impassable = 1 //непроходимая
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

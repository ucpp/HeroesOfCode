﻿using UnityEngine;

namespace Maryan.HeroesOfCode
{
    [CreateAssetMenu(fileName = nameof(UnitRuntimeSet), menuName = EditorUtils.UtilsMenu + nameof(UnitRuntimeSet))]
    public class UnitRuntimeSet : RuntimeSet<UnitBehaviour>
    {
        public UnitBehaviour GetUnitByPoint(Point point)
        {
            return Items.Find(unit => unit.Position == point);
        }
    }
}

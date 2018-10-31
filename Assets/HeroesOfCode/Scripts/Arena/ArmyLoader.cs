using System.Collections.Generic;
using UnityEngine;

namespace Maryan.HeroesOfCode
{
    public class ArmyLoader
    { 
        public List<SquadBehaviour> Load(Army army, StartPoints startPoints, bool isOwn = false)
        {
            if(army == null)
            {
                Debug.Log("Army is null!");
                return new List<SquadBehaviour>();
            }

            int squadIndex = 0;
            List<SquadBehaviour> unitsList = new List<SquadBehaviour>();
            foreach(var squad in army.Squads)
            {
                if(squad.CurrentCount == 0)
                {
                    continue;
                }
                var unit = GameObject.Instantiate(squad.Unit.Prefab);
                var unitBehaviour = unit.GetComponent<SquadBehaviour>();
                unitBehaviour.Init(squad, isOwn);
                unitBehaviour.Position = startPoints.GetPointByIndex(squadIndex);
                unit.transform.position = startPoints.GetPositionByIndex(squadIndex);
                unitsList.Add(unitBehaviour);
                squadIndex++;
            }
            return unitsList;
        }
    }
}
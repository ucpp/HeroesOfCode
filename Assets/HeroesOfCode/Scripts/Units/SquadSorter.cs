using UnityEngine;

namespace Maryan.HeroesOfCode
{
    public sealed class SquadSorter : MonoBehaviour
    {
        private void Start()
        {
            var renderers = GetComponentsInChildren<SpriteRenderer>();
            foreach(var renderer in renderers)
            {
                renderer.sortingOrder = Mathf.RoundToInt(transform.position.y * 100f) * -1;
            }
        }
    }
}

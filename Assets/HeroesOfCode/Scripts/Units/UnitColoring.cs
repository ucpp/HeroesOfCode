using UnityEngine;

namespace Maryan.HeroesOfCode
{
    [CreateAssetMenu(fileName = nameof(UnitColoring), menuName = EditorUtils.Modificators + nameof(UnitColoring))]
    public sealed class UnitColoring : UnitModificator
    {
        [SerializeField]
        private Color _activeColor;
        [SerializeField]
        private Color _defaultColor;
        [SerializeField]
        private LayerMask _layers;

        public override void StartExecute(Transform transform)
        {
            SetColor(transform, _activeColor);
        }

        public override void Stop(Transform transform)
        {
            SetColor(transform, _defaultColor);
        }

        private void SetColor(Transform transform, Color color)
        {
            var spriteRenderers = GetRenderers(transform);
            if(spriteRenderers != null)
            {
                foreach(var spriteRenderer in spriteRenderers)
                {
                    if(_layers.Contains(spriteRenderer.gameObject.layer))
                    {
                        spriteRenderer.color = color;
                    }
                }
            }
        }

        private SpriteRenderer[] GetRenderers(Transform transform)
        {
            if(transform != null)
            {
                return transform.GetComponentsInChildren<SpriteRenderer>();
            }
            return null;
        }
    }
}

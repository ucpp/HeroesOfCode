using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Maryan.HeroesOfCode
{
    public class PawnMover : MonoBehaviour
    {
        [SerializeField]
        private Way _way;
        [SerializeField]
        private WayCreator _wayCreator;
        [SerializeField]
        private float _speed;
        [SerializeField]
        private UnityEvent _onStartMove;
        [SerializeField]
        private UnityEvent _onEndMove;
        [SerializeField]
        private UnityEvent _onEndStep;

        private bool _isRuning;

        public void StartRun()
        {
            if(!_isRuning)
            {
                _isRuning = true;
                StartCoroutine(Run());
            }
        }

        private IEnumerator Run()
        {
            _onStartMove.Invoke();
            var positions = _way.GetWorldPoints();

            for(int i = 0; i < positions.Length - 1; i++)
            {
                _wayCreator.HideDot(_way.Path[i]);
                var distance = Vector2.Distance(positions[i], positions[i + 1]);
                float t = 1 / (distance / _speed);
                float dt = 0;
                while(dt <= 1.0f)
                {
                    dt += Time.deltaTime * t;
                    transform.position = Vector2.Lerp(positions[i], positions[i + 1], dt);
                    yield return new WaitForEndOfFrame();
                }
                _onEndStep.Invoke();
            }
            _way.Start = _way.End;
            _onEndMove.Invoke();
            _isRuning = false;
        }
    }
}

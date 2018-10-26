using UnityEngine;
using UnityEngine.Events;

namespace Maryan.HeroesOfCode
{
    public class Arena : MonoBehaviour
    {
        [SerializeField]
        private BattleBehaviour _playerBattleBehaviour;
        [SerializeField]
        private BattleBehaviour _computerBattleBehaviour;
        [SerializeField]
        private UnitRuntimeSet _unitRuntimeSet;
        [SerializeField]
        private UnityEvent _onWinBattle;
        [SerializeField]
        private UnityEvent _onLoseBattle;

        private void Start()
        {
            _playerBattleBehaviour.Initialize();
            _computerBattleBehaviour.Initialize();
            OnEndComputerAttack();
        }

        private void OnEndPlayerAttack()
        {
            _playerBattleBehaviour.OnEndAttack.RemoveListener(OnEndPlayerAttack);
            _computerBattleBehaviour.CheckState();
            if(_computerBattleBehaviour.IsDie)
            {
                //end fight, you win!
                _onWinBattle.Invoke();
            }
            else
            {
                _computerBattleBehaviour.OnEndAttack.AddListener(OnEndComputerAttack);
                _computerBattleBehaviour.StartAttack(_playerBattleBehaviour.Army);
            }
        }

        private void OnEndComputerAttack()
        {
            _computerBattleBehaviour.OnEndAttack.RemoveListener(OnEndComputerAttack);
            _playerBattleBehaviour.CheckState();
            if(_playerBattleBehaviour.IsDie)
            {
                //end fight, you lose
                _onLoseBattle.Invoke();
            }
            else
            {
                _playerBattleBehaviour.OnEndAttack.AddListener(OnEndPlayerAttack);
                _playerBattleBehaviour.StartAttack(_computerBattleBehaviour.Army);
            }
        }

        private void Update()
        {
            _computerBattleBehaviour.Update();
            _playerBattleBehaviour.Update();
        }

        private void OnDestroy()
        {
            _playerBattleBehaviour.EndBattle();
            _computerBattleBehaviour.EndBattle();
        }
    }
}
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
        private SquadRuntimeSet _unitRuntimeSet;
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
                //конец боя, вы победили!
                _onWinBattle.Invoke();
            }
            else
            {
                _computerBattleBehaviour.OnEndAttack.AddListener(OnEndComputerAttack);
                _computerBattleBehaviour.StartAttack();
            }
        }

        private void OnEndComputerAttack()
        {
            _computerBattleBehaviour.OnEndAttack.RemoveListener(OnEndComputerAttack);
            _playerBattleBehaviour.CheckState();
            if(_playerBattleBehaviour.IsDie)
            {
                //конец боя вы проиграли
                _onLoseBattle.Invoke();
            }
            else
            {
                _playerBattleBehaviour.OnEndAttack.AddListener(OnEndPlayerAttack);
                _playerBattleBehaviour.StartAttack();
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
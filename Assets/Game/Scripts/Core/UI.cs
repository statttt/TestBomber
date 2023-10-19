using Game.Scripts.GamePlay.Setup;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VContainer;

namespace Game.Scripts.Core
{
    public class UI : MonoBehaviour
    {
        [SerializeField]
        private List<BombCounter> _bombCounters = new List<BombCounter>();
        [SerializeField]
        private Transform _bombCountersParent;
        [SerializeField]
        private BombCounter _bombCounterPrefab;

        private GameControl _gameControl;

        private BombCounter _activeBombCounter;

        private int _activeBombCounterIndex;

        public void Initialize(GameControl gameControl)
        {
            _gameControl = gameControl;
        }

        private void Start()
        {
            CreateCounter();
        }

        public void Update()
        {
            ChooseBombCounter();
        }

        public void ChangeActiveBombCounter(BombType type)
        {
            if(_activeBombCounter != null)
            {
                _activeBombCounter.Deactivate();;
            }

            _activeBombCounter = GetBombCounter(type);
            _activeBombCounter.Activate();
            _gameControl.ActiveBombType = _activeBombCounter.Type;
        }

        public void CreateCounter()
        {
            List<BombInfo> bombList = _gameControl.BombFactory.BombList;
            foreach (var bomb in bombList)
            {
                BombCounter bombCounter = Instantiate(_bombCounterPrefab, _bombCountersParent);
                bombCounter.Initialize(bomb);
                _bombCounters.Add(bombCounter);
            }
            ChangeActiveBombCounter(_gameControl.ActiveBombType);
        }

        public void AddBomb(BombType type)
        {
            BombCounter bombCounter = _bombCounters.Find(x => x.Type == type);
            if (bombCounter != null)
            {
                bombCounter.AddBomb();
            }
        }

        public BombCounter GetBombCounter(BombType type)
        {
            BombCounter bombCounter = _bombCounters.Find(x => x.Type == type);
            return bombCounter;
        }

        public void ChooseBombCounter()
        {
            if(Input.GetKeyDown(KeyCode.LeftArrow))
            {
                if(_activeBombCounterIndex <= 0)
                {
                    _activeBombCounterIndex = _bombCounters.Count - 1;
                }
                else
                {
                    _activeBombCounterIndex--;
                }
                ChangeActiveBombCounter(_bombCounters[_activeBombCounterIndex].Type);
            }
            if(Input.GetKeyDown(KeyCode.RightArrow))
            {
                if(_activeBombCounterIndex >= _bombCounters.Count - 1)
                {
                    _activeBombCounterIndex = 0;
                }
                else
                {
                    _activeBombCounterIndex++;
                }
                ChangeActiveBombCounter(_bombCounters[_activeBombCounterIndex].Type);
            }
        }

    }
}

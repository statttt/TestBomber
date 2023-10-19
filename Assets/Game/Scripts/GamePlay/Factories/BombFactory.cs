using Cysharp.Threading.Tasks;
using Game.GamePlay.Enemy;
using Game.Scripts.Core;
using Game.Scripts.GamePlay;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game.Scripts.GamePlay.Setup
{
    [CreateAssetMenu]
    public class BombFactory : GameObjectFactory
    {
        [SerializeField]
        private Bomb _bombPrefab;
        [SerializeField]
        private List<BombInfo> _bombInfoList = new List<BombInfo>();
        [SerializeField]
        private float _respawnBombDelay;

        private Player _player;
        private GameControl _gameControl;

        public List<BombInfo> BombList => _bombInfoList;
        

        public void Initialize(Player player, GameControl gameControl)
        {
            _player = player;
            _gameControl = gameControl;
        }

        public Bomb Get()
        {
            Bomb bomb = CreateGameObjectInstance(_bombPrefab);
            int randomType = Random.Range(0, Enum.GetNames(typeof(BombType)).Length);
            BombInfo bombInfo = _bombInfoList.Find(x => x.Type == (BombType)randomType);
            bomb.Initialize(_player, this, bombInfo);
            return bomb;
        }

        public async void Reclaim(Bomb bomb)
        {
            Vector3 lastPos = bomb.transform.position;
            Destroy(bomb.gameObject);
            await UniTask.Delay(TimeSpan.FromSeconds(_respawnBombDelay));
            _gameControl.SpawnBomb(lastPos);
        }

        public BombInfo GetBombInfoByType(BombType type)
        {
            BombInfo info = _bombInfoList.Find(x => x.Type == type);
            return info;
        }
    }
}


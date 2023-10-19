using Game.GamePlay.Enemy;
using Game.Scripts.GamePlay;
using Game.Scripts.GamePlay.Setup;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VContainer;

namespace Game.Scripts.Core
{
    public class GameControl : MonoBehaviour
    {
        [SerializeField]
        private Transform _ground;
        [SerializeField]
        public float _spawnOffset;
        [SerializeField]
        private EnemyFactory _enemyFactory;
        [SerializeField]
        private int _startEnemyCount;
        [SerializeField]
        private BombFactory _bombFactory;
        [SerializeField]
        private int _startBombCount;

        private Player _player;
        private UI _UIManager;
        
        private List<Enemy> _enemyList = new List<Enemy>();
        private List<Bomb> _bombList = new List<Bomb>();

        public BombFactory BombFactory => _bombFactory;
        private IInput _input;
        private CameraMovement _camera;
        public BombType ActiveBombType { get; set; }

        [Inject]
        public void Construct(Player player, UI UIManager, IInput input, CameraMovement camera)
        {
            _player = player;
            _UIManager = UIManager;
            _input = input;
            _camera = camera;
            Initialize();
        }

        public void Initialize()
        {
            _enemyFactory.Initialize(_player, this);
            _bombFactory.Initialize(_player, this);
            _UIManager.Initialize(this);
            _player.Initialize(this, _input);
            _camera.Initialize(_player);
        }

        private void Start()
        {
            ActiveBombType = BombType.Small;
            StartGame();
        }

        public void StartGame()
        {
            for (int i = 0; i < _startEnemyCount; i++)
            {
                SpawnEnemy(GetRandomPointOnGraound());
            }
            for (int i = 0; i < _startBombCount; i++)
            {
                SpawnBomb(GetRandomPointOnGraound());
            }
        }

        public void SpawnEnemy(Vector3 spawnPosition)
        {
            Enemy enemy = _enemyFactory.Get();
            enemy.transform.position = spawnPosition;
            _enemyList.Add(enemy);
        }

        public void SpawnBomb(Vector3 spawnPosition)
        {
            Bomb bomb = _bombFactory.Get();
            bomb.transform.position = spawnPosition;
            _bombList.Add(bomb);
        }

        public Vector3 GetRandomPointOnGraound()
        {
            float randoX = Random.Range(_ground.transform.position.x - _spawnOffset,
                                        _ground.transform.position.x + _spawnOffset);
            float randoZ = Random.Range(_ground.transform.position.z - _spawnOffset,
                                        _ground.transform.position.z + _spawnOffset);
            Vector3 randomPointOnGround = new Vector3(randoX, _ground.transform.position.y, randoZ);
            return randomPointOnGround;
        }


        public void AddBomb(BombType type)
        {
            _UIManager.AddBomb(type);
        }

        public bool IsHaveBomb()
        {
            int count = _UIManager.GetBombCounter(ActiveBombType).Count;
            return count > 0;
        }

        public BombInfo GetActiveBombInfo()
        {
            BombInfo info = _bombFactory.GetBombInfoByType(ActiveBombType);
            return info;
        }

        public void RemoveBomb()
        {
            _UIManager.GetBombCounter(ActiveBombType).RemoveBomb();
        }
    }


    
}


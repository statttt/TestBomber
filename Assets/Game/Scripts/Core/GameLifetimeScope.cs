using VContainer;
using VContainer.Unity;
using Game.Scripts.GamePlay;
using UnityEngine;
using Unity.VisualScripting;
using Game.Scripts;
using Game.Scripts.Core;

namespace Game.Scripts
{

    public sealed class GameLifetimeScope : LifetimeScope
    {
        [SerializeField]
        private Player _playerPrefab;
        [SerializeField]
        private PlayerSpawner _playerSpawner;
        [SerializeField]
        private GameControl _game;
        [SerializeField]
        private UI _UI;
        [SerializeField]
        private CameraMovement _camera;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<KeyboardInput>(Lifetime.Scoped).As<ITickable, IInput>();
            builder.RegisterComponentInNewPrefab<Player>(_playerPrefab, Lifetime.Singleton);
            builder.RegisterComponentInNewPrefab<UI>(_UI, Lifetime.Singleton);
            builder.RegisterComponent(_playerSpawner);
            builder.RegisterComponent(_game);
            builder.RegisterComponent(_camera);

        }
    }
}


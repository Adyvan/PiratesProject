using Prototype.Scripts.Components;
using Prototype.Scripts.Parts.Player;
using Prototype.Scripts.Settings.GamePlay;
using Scellecs.Morpeh;
using Scellecs.Morpeh.Systems;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;

namespace Prototype.Scripts.Systems
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    [CreateAssetMenu(menuName = "ECS/Systems/" + nameof(PlayerSpawnSystem))]
    public sealed class PlayerSpawnSystem : UpdateSystem
    {
        [SerializeField]
        private PrefabsSettings _settings;

        private PlayerPool _playerPool;
        private Stash<GameUser> _gameUserStash;

        public override void OnAwake()
        {
            _playerPool = new PlayerPool(_settings.PlayerViewPrefab, default);
            _playerPool.Initialize();

            _gameUserStash = World.GetStash<GameUser>().AsDisposable();
        }

        public override void OnUpdate(float deltaTime)
        {
            var damageRequest = World.GetRequest<NewInputSourceEvent>();
            
            // handle request
            foreach (var request in damageRequest.Consume())
            {
                request.inputActions.Enable();
                Debug.Log("Init Player");
                Spawn(request);
            }
        }

        private void Spawn(NewInputSourceEvent newInputSourceEvent)
        {
            var playerView = _playerPool.GetItem();
            playerView.InitObject(_playerPool, RemoveAllPlayerComponents);

            var entity = playerView.Entity;
            ref var playerUnit = ref entity.AddComponent<PlayerUnit>();
            playerUnit.PlayerView = playerView;

            ref var health = ref entity.AddComponent<HealthComponent>();
            health.healthPoints = 100;

            
            ref var gameUser = ref _gameUserStash.Add(entity);
            {
                (gameUser.inputActions, newInputSourceEvent.inputActions) =
                    (newInputSourceEvent.inputActions, gameUser.inputActions);
                (gameUser.device, newInputSourceEvent.device) = (newInputSourceEvent.device, gameUser.device);
                (gameUser.user, newInputSourceEvent.user) = (newInputSourceEvent.user, gameUser.user);
            }
        }

        private static void RemoveAllPlayerComponents(PlayerView playerView)
        {
            //TODO
        }
    }
}
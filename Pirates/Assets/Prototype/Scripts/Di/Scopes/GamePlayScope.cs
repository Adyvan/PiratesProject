using Prototype.Scripts.Parts.Player;
using Prototype.Scripts.Settings;
using Prototype.Scripts.Settings.GamePlay;
using Scellecs.Morpeh.Elysium;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Prototype.Scripts.Di.Scopes
{
    public class GamePlayScope : LifetimeScope
    {
        [SerializeField] private GamePlaySettings _gamePlaySettings;

        [SerializeField] private GamePlaySceneRegistry _gamePlaySceneRegistry;

        protected override void Configure(IContainerBuilder builder)
        {
            // builder.RegisterEntryPoint<EcsModule>();
            builder.Register<PlayerPool>(Lifetime.Singleton)
                .WithParameter(_gamePlaySettings.PrefabsSettings.PlayerViewPrefab)
                .WithParameter(_gamePlaySceneRegistry.PlayerViewParent);
        }
    }

}
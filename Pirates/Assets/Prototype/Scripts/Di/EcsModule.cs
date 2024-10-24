// using System;
// using Prototype.Scripts.Systems;
// using Scellecs.Morpeh.Elysium;
// using UnityEngine.InputSystem;
// using VContainer;
// using VContainer.Unity;
//
// namespace Prototype.Scripts.Di
// {
//     public class EcsModule : IStartable, IDisposable
//     {
//         private readonly LifetimeScope _scope;
//         private EcsStartup _startup;
//
//         [Inject]
//         public EcsModule(LifetimeScope scope)
//         {
//             this._scope = scope;
//         }
//
//         public void Start()
//         {
//             _startup = new EcsStartup(new VContainerResolver(_scope));
//
//             _startup
//                 .AddSystemsGroup()
//                 .AddInitializerInjected<GameInitializer>()
//                 .AddUpdateSystemInjected<GameInputSystem>();
//
//             _startup
//                 .AddSystemsGroup()
//                 .AddFeatureInjected<AnimationFeature>()
//                 .AddFeatureInjected<RenderFeature>();
//
//             _startup
//                 .AddSystemsGroup()
//                 .AddCleanupSystem(new DestroyEntitySystem());
//
//             _startup.Initialize(updateByUnity: true);
//         }
//
//         public void Dispose()
//         {
//             _startup?.Dispose();
//         }
//     }
//
//     public sealed class AnimationFeature : IEcsFeature
//     {
//         private readonly IGameSettingsService gameSettings;
//
//         [Inject]
//         public AnimationFeature(IGameSettingsService gameSettings)
//         {
//             this.gameSettings = gameSettings;
//         }
//
//         public void Configure(EcsStartup.FeatureBuilder builder)
//         {
//             builder
//                 .AddUpdateSystemInjected<AnimatorInitializeSystem>()
//                 .AddUpdateSystemInjected<IdleAnimationSystem>()
//                 .AddUpdateSystemInjected<MovementAnimationSystem>()
//                 .AddUpdateSystemInjected<DieAnimationSystem>();
//
//             if (gameSettings.Graphics.EnableExperimentalAnimations)
//             {
//                 builder.AddUpdateSystem(new ExperimentalAnimatorSystem(...));
//             }
//             else
//             { 
//                 builder.AddUpdateSystemInjected<AnimatorSystem>();
//             }
//         }
//     }
// }
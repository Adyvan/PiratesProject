using Prototype.Scripts.Components;
using Scellecs.Morpeh;
using Scellecs.Morpeh.Systems;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;

namespace Prototype.Scripts.Systems
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    [CreateAssetMenu(menuName = "ECS/Systems/" + nameof(HealthSystem))]
    public sealed class HealthSystem : UpdateSystem
    {
        private Filter _filter;
        private Stash<HealthComponent> _healthStash;
    
        public override void OnAwake() {
            _filter = World.Filter.With<HealthComponent>().Build();
            _healthStash = World.GetStash<HealthComponent>();
        }

        public override void OnUpdate(float deltaTime) {
            foreach (var entity in _filter) {
                ref var healthComponent = ref _healthStash.Get(entity);
                Debug.Log(healthComponent.healthPoints);
            }
        }
    }
}
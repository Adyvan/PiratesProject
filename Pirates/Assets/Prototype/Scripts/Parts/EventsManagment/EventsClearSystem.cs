// using Prototype.Scripts.Components;
// using Scellecs.Morpeh;
// using Scellecs.Morpeh.Systems;
// using Unity.IL2CPP.CompilerServices;
// using UnityEngine;
//
// namespace Prototype.Scripts.Parts.EventsManagment
// {
//     [Il2CppSetOption(Option.NullChecks, false)]
//     [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
//     [Il2CppSetOption(Option.DivideByZeroChecks, false)]
//     [CreateAssetMenu(menuName = "ECS/Systems/" + nameof(EventsClearSystem))]
//     public sealed class EventsClearSystem : LateUpdateSystem 
//     {
//         private Filter _newInputSourceEventFilter;
//         private Stash<NewInputSourceEvent> _newInputSourceEventStash;
//     
//         public override void OnAwake()
//         {
//             _newInputSourceEventFilter = World.Filter.With<NewInputSourceEvent>().Build();
//             _newInputSourceEventStash = World.GetStash<NewInputSourceEvent>().AsDisposable();
//         }
//
//         public override void OnUpdate(float deltaTime)
//         {
//             foreach (var entity in _newInputSourceEventFilter)
//             {
//                 ref var ev = ref _newInputSourceEventStash.Get(entity);
//                 if (!ev.Handled) continue;
//
//                 _newInputSourceEventStash.Remove(entity);
//                 World.RemoveEntity(entity);
//             }
//         }
//
//         public override void Dispose()
//         {
//             foreach (var entity in _newInputSourceEventFilter)
//             {
//                 _newInputSourceEventStash.Remove(entity);
//                 World.RemoveEntity(entity);
//             }
//             base.Dispose();
//         }
//     }
// }
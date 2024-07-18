using System;
using Prototype.Scripts.Components;
using Scellecs.Morpeh;
using Scellecs.Morpeh.Systems;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.InputSystem.Users;

namespace Prototype.Scripts.Systems
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    [CreateAssetMenu(menuName = "ECS/Systems/" + nameof(GameInputSystem))]
    public sealed class GameInputSystem : UpdateSystem
    {
        private Action<InputControl, InputEventPtr> unpairedDeviceUsedDelegate;
        
        public override void OnAwake()
        {
            World.GetStash<NewInputSourceEvent>().AsDisposable();

            unpairedDeviceUsedDelegate = OnUnpairedDeviceUsed;
            ++InputUser.listenForUnpairedDeviceActivity;
            InputUser.onUnpairedDeviceUsed += unpairedDeviceUsedDelegate;
        }

        public override void OnUpdate(float deltaTime)
        {
            InputSystem.Update();
        }

        public override void Dispose()
        {
            InputUser.onUnpairedDeviceUsed -= unpairedDeviceUsedDelegate;
            --InputUser.listenForUnpairedDeviceActivity;
        }

        private void OnUnpairedDeviceUsed(InputControl control, InputEventPtr eventPtr)
        {
            if (!(control is ButtonControl))
            {
                return;
            }

            var actions = new InputActions();
             if (!actions.CommonScheme.SupportsDevice(control.device))
             {
                 return;
             }
            
             Entity userEntity = World.CreateEntity();
             ref NewInputSourceEvent newInputSource = ref userEntity.AddComponent<NewInputSourceEvent>();
             newInputSource.device = control.device;
            
             newInputSource.inputActions = actions;
             newInputSource.inputActions.Enable();
            
             newInputSource.user = InputUser.PerformPairingWithDevice(control.device);
             newInputSource.user.ActivateControlScheme(actions.CommonScheme);
             newInputSource.user.AssociateActionsWithUser(actions);
        }
    }
}
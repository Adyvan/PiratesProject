using Scellecs.Morpeh;
using Unity.IL2CPP.CompilerServices;
using System;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;

namespace Prototype.Scripts.Components
{
    [System.Serializable]
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public struct GameUser : IComponent, IDisposable {
        public InputDevice device;
        public InputActions inputActions;
        public InputUser user;
        public int id;

        public void Dispose() {
            inputActions?.Disable();

            if (!user.valid) {
                return;
            }

            user.UnpairDevicesAndRemoveUser();
        }
    }
}
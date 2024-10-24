using System;
using Scellecs.Morpeh;
using Unity.IL2CPP.CompilerServices;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;

namespace Prototype.Scripts.Components
{
    [System.Serializable]
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public struct NewInputSourceEvent : IRequestData, IDisposable
    {
        public InputDevice device;
        public InputActions inputActions;
        public InputUser user;

        public void Dispose() 
        {
            inputActions?.Disable();

            if (user != default && user.valid) 
            {
                user.UnpairDevicesAndRemoveUser();
            }

        }
    }
}
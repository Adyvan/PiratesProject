using System;
using Prototype.Scripts.Parts.Player;
using UnityEngine;

namespace Prototype.Scripts.Settings.GamePlay
{
    [Serializable]
    public class PrefabsSettings
    {
        [field: SerializeField] 
        public PlayerView PlayerViewPrefab { get; private set; }
    }
}
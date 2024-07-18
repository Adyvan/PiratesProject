using System;
using Prototype.Scripts.Settings.GamePlay;
using UnityEngine;

namespace Prototype.Scripts.Settings
{
    [CreateAssetMenu(fileName = "ScriptableObjects", menuName = "GamePlaySettings", order = 0)]
    [Serializable]
    public class GamePlaySettings : ScriptableObject
    {
        [field: SerializeField] public PrefabsSettings PrefabsSettings { get; private set; }
    }
}
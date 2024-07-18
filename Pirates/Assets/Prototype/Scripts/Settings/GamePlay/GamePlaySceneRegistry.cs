using UnityEngine;

namespace Prototype.Scripts.Settings.GamePlay
{
    public class GamePlaySceneRegistry : MonoBehaviour
    {
        [field: SerializeField] 
        public Transform PlayerViewParent { get; private set; }
    }
}
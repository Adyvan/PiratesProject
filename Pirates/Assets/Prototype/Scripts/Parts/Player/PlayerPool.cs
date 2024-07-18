using Prototype.Scripts.Common.Pools;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Prototype.Scripts.Parts.Player
{
    public class PlayerPool : ConstSizePool<PlayerView>
    {
        public PlayerPool(PlayerView prefab, Transform root) : base(2, () => Instantiate(prefab, default))
        {
        }

        private static PlayerView Instantiate(PlayerView prefab, Transform root)
        {
            // var view = Object.Instantiate(prefab, root);
            var view = Object.Instantiate(prefab);
            return view;
        }
    }
}
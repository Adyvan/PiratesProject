using System;
using Prototype.Scripts.Common.Pools;
using Scellecs.Morpeh;
using Scellecs.Morpeh.Providers;
using UnityEngine;

namespace Prototype.Scripts.Parts.Player
{
    [RequireComponent(typeof(EntityProvider))]
    public class PlayerView : MonoBehaviour, IPoolableObject
    {
        private IPool<PlayerView> _pool;
        public int PoolObjectId { get; set; }

        private Action<PlayerView> _removeComponents;

        [SerializeField]
        private EntityProvider _entityProvider;
        private void Reset()
        {
            _entityProvider = GetComponent<EntityProvider>();
        }

        public Entity Entity => _entityProvider.Entity;

        public void InitObject(IPool<PlayerView> pool, Action<PlayerView> removeComponents)
        {
            _pool = pool;
            _removeComponents = removeComponents;
            gameObject.SetActive(true);
        }

        public void Release()
        {
            gameObject.SetActive(false);
            //todo remove all components
            _removeComponents?.Invoke(this);
            _removeComponents = null;

            _pool.Release(this);
        }
    }
}
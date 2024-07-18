namespace Prototype.Scripts.Common.Pools
{
    public interface IPool<T>
    {
        T GetItem();
        void Release(T obj);
    }
}
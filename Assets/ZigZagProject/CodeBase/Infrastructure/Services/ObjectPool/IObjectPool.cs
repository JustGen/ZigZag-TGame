using System;
using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Logic;

namespace CodeBase.Infrastructure.Services.ObjectPool
{
    public interface IObjectPool : IService
    {
        event Action OnReturnToPool;
        void CreateFreeObject(int count);
        ItemGround GetItem();
        void ReturnToPool(ItemGround item);
    }
}
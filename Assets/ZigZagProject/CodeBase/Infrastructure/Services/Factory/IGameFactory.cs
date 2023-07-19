using CodeBase.Infrastructure.AssetManagement;
using UnityEngine;

namespace CodeBase.Infrastructure.Services.Factory
{
    public interface IGameFactory : IService
    {
        GameObject CreateItem(Transform parent);
        GameObject CreateItem(Vector3 at, Transform parent = null);
    }
}
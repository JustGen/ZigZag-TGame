using CodeBase.Infrastructure.AssetManagement;
using UnityEngine;

namespace CodeBase.Infrastructure.Services.Factory
{
    public class GameFactory : IGameFactory
    {
        private readonly IAssetProvider _assets;

        public GameFactory(IAssetProvider assets) => 
            _assets = assets;

        public GameObject CreateItem(Transform parent) => 
            Object.Instantiate(_assets.GetItem(), parent);

        public GameObject CreateItem(Vector3 at, Transform parent)
        {
            GameObject itemGameObject = Object.Instantiate(_assets.GetItem(), at, Quaternion.identity, parent);
            return itemGameObject;
        }
    }
}
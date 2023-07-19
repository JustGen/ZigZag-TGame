using UnityEngine;

namespace CodeBase.Infrastructure.AssetManagement
{
    public class AssetProvider : IAssetProvider
    {
        public GameObject GetItem() => 
            Resources.Load<GameObject>(AssetPath.ItemGround);
    }
}
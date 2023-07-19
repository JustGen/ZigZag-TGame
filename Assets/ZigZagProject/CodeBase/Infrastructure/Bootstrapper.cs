using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.Services.Factory;
using CodeBase.Infrastructure.Services.ObjectPool;
using CodeBase.Infrastructure.Services.PersistentProgress;
using CodeBase.Infrastructure.Services.Settings;
using CodeBase.Infrastructure.Services.States;
using CodeBase.Infrastructure.UI;
using UnityEngine;

namespace CodeBase.Infrastructure
{
    public class Bootstrapper : MonoBehaviour
    {
        private void Awake()
        {
            RegisterServices();
        }

        private void RegisterServices()
        {
            AllServices.Container.RegisterSingle<IAssetProvider>(new AssetProvider());
            AllServices.Container.RegisterSingle<IGameFactory>(new GameFactory(AllServices.Container.Single<IAssetProvider>()));
            AllServices.Container.RegisterSingle<IObjectPool>(new ObjectPool(AllServices.Container.Single<IGameFactory>()));
            AllServices.Container.RegisterSingle<ISwitcherPanelUI>(new SwitcherPanelUI());
            AllServices.Container.RegisterSingle<IGameStateInfo>(new GameStateInfo());
            AllServices.Container.RegisterSingle<IPersistentProgressService>(new PersistentProgressService());
            AllServices.Container.RegisterSingle<ISettingsService>(new SettingsService());
        }
    }
}
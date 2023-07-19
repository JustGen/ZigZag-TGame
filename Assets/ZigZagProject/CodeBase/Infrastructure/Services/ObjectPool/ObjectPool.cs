using System;
using System.Collections.Generic;
using CodeBase.Infrastructure.Services.Factory;
using CodeBase.Logic;
using UnityEngine;

namespace CodeBase.Infrastructure.Services.ObjectPool
{
    public class ObjectPool : IObjectPool
    {
        private readonly List<ItemGround> _availableObjects;
        private readonly IGameFactory _factory;
        private readonly Transform _container;

        public event Action OnReturnToPool;
        
        public ObjectPool(IGameFactory factory)
        {
            _container = new GameObject().transform;
            _container.name = "ContainerItemsGround";
            _factory = factory;
            _availableObjects = new List<ItemGround>();
        }
        
        public void CreateFreeObject(int count)
        {
            for (int i = 0; i < count; i++)
            {
                ItemGround item = CreateItemFromFactory();
                item.gameObject.name += i.ToString();
                _availableObjects.Add(item);
                item.GetGameObject.SetActive(false);
            }
        }

        public ItemGround GetItem()
        {
            ItemGround item;

            if (_availableObjects.Count > 0)
            {
                item = _availableObjects[0];
                _availableObjects.Remove(item);
            }
            else
            {
                item = CreateItemFromFactory();
            }

            item.GetGameObject.SetActive(true);
            item.OnHidden += ReturnToPool;
            return item;
        }

        public void ReturnToPool(ItemGround item)
        {
            item.OnHidden -= ReturnToPool;
            _availableObjects.Add(item);
            item.GetGameObject.SetActive(false);
            item.GetGameObject.transform.parent = _container;
            
            OnReturnToPool?.Invoke();
        }

        private ItemGround CreateItemFromFactory() => 
            _factory.CreateItem(_container).GetComponent<ItemGround>();
    }
}